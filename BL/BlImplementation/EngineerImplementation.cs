
namespace BlImplementation;
using BlApi;
using BO;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

     public void Create(BO.Engineer boEngineer)
    {
        try
        {
            DO.Engineer doEngineer = BOToDO(boEngineer);
            _dal.Engineer.Create(doEngineer);
        }
        catch (BlIncorrectDetails ex)
        {
            throw ex;
        }
        catch {
            throw new BO.BlAlreadyExistsException($"Engineer number {boEngineer.ID} exists");
        }
    }

    public void Delete(int id)
    {
        try
        {
            if (_dal.Task.ReadAll().Any(task => task.IDEngineer == id))
            {
                throw new BO.EngineerHaveTask("There are tasks that rely on it");
            }
            _dal.Engineer.Delete(id);
        }
        catch(EngineerHaveTask ex)
        {
            throw ex;
        }
        catch
        {
            throw new BlDoesNotExistException($"Engineer id {id} dos't exist");
        }
    }

    public BO.Engineer? Read(int id)
    {
        try
        {
            DO.Engineer? myEngineer = _dal.Engineer.Read(id);
            return DOToBO(myEngineer);
        }
        catch {
            throw new BlDoesNotExistException($"Engineer id {id} dos't exist");
        }
    }

    public IEnumerable<BO.Engineer> ReadAll(BO.EngineerLevelEnum level = EngineerLevelEnum.None)
    {
        if(level == EngineerLevelEnum.None)
        {
            return from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                   select DOToBO(doEngineer);
        }
        return from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
               where (BO.EngineerLevelEnum)doEngineer.EngineerLevel == level
               select DOToBO(doEngineer);
    }

    public void Update(BO.Engineer boEngineer)
    {
        try
        {
            _dal.Engineer.Update(BOToDO(boEngineer));
        }
        catch (BlIncorrectDetails ex)
        {
            throw ex;
        }
        catch
        {
            throw new BlDoesNotExistException($"Engineer id {boEngineer.ID} dos't exist");
        }
    }

    private BO.Engineer DOToBO(DO.Engineer doEngineer)
    {
        return new BO.Engineer
        {
            ID = doEngineer.ID,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            EngineerLevel = (BO.EngineerLevelEnum)doEngineer.EngineerLevel,
            PriceOfHour = doEngineer.PriceOfHour,
            Task = (from DO.Task doTask in _dal.Task.ReadAll()
                           where doTask.IDEngineer == doEngineer.ID
                           select new BO.TaskIdNickname() { ID = doTask.ID, Nickname = doTask.Nickname! }).FirstOrDefault()
        };
    }

    private DO.Engineer BOToDO(BO.Engineer boEngineer)
    {
        if (boEngineer.ID <= 0 || string.IsNullOrEmpty(boEngineer.Name) || boEngineer.PriceOfHour > 0 || string.IsNullOrEmpty(boEngineer.Email))
        {
            throw new BlIncorrectDetails("The Detals are incorrect");
        }
        return new DO.Engineer { ID = boEngineer.ID, Name = boEngineer.Name, Email = boEngineer.Email, EngineerLevel = (DO.EngineerLevelEnum)boEngineer.EngineerLevel, PriceOfHour = boEngineer.PriceOfHour };
    }
}
