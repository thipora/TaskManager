
namespace BlImplementation;
using BlApi;
using BO;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Create(BO.Task boTask)
    {
        try
        {
            DO.Task doTask = BOToDO(boTask);
            _dal.Task.Create(doTask);
        }
        catch (BO.BlNullPropertyException ex)
        {
            throw ex;
        }
        catch
        {
            throw new BO.BlAlreadyExistsException($"Task number {boTask.ID} exists");
        }
    }
    public void Delete(int id)
    {
        try
        {
            if (_dal.Dependence.ReadAll().Any(dependence => dependence!.IDPreviousTask == id))
            {
                throw new BO.BlDependesOnIt("There are tasks that rely on it");
            }
            _dal.Task.Delete(id);
        }
        catch (BO.BlDependesOnIt ex)
        {
            throw ex;
        }
        catch
        {
            throw new BlDoesNotExistException($"Task number {id} dos't exist");
        }
    }

    public BO.Task? Read(int id)
    {
        try
        {
            DO.Task? myTask = _dal.Task.Read(id);
            return DOToBO(myTask);
        }
        catch
        {
            throw new BlDoesNotExistException($"Task number {id} dos't exist");
        }
    }

    public IEnumerable<BO.Task> ReadAll()
    {
        return _dal.Task.ReadAll()
               .Select(doTask => DOToBO(doTask!));
    }

    public void Update(BO.Task boTask)
    {
        try
        {
            _dal.Task.Update(BOToDO(boTask));
        }
        catch (BO.BlNullPropertyException ex)
        {
            throw ex;
        }
        catch
        {
            throw new BlDoesNotExistException($"Task number {boTask.ID} dos't exist");
        }
    }

    private BO.Task DOToBO(DO.Task doTask)
    {
        return new BO.Task
        {
            ID = doTask.ID!,
            Nickname = doTask.Nickname!,
            Description = doTask.Description,
            Production = doTask.Production,
            TaskStatus = FindStatus(doTask),
            TaskList = FindTaskList(doTask.ID),
            RelatedMilestone = findMilestone(doTask.ID),
            EstimatedStartDate = doTask.EstimatedStartDate,
            AcualStartNate = doTask.AcualStartNate,
            EstimatedEndDate = doTask.EstimatedEndDate,
            deadline = doTask.deadline,
            AcualEndNate = doTask.AcualEndNate,
            Product = doTask.Product,
            Remaeks = doTask.Remaeks,
            Difficulty = (BO.EngineerLevelEnum)doTask.Difficulty,
            EngineerIdName = findEngineer(doTask.IDEngineer)
        };
    }

    private DO.Task BOToDO(BO.Task boTask)
    {
        if (boTask.ID <= 0 || string.IsNullOrEmpty(boTask.Nickname))
        {
            throw new BlIncorrectDetails("ID and Nickname must have valid values");
        }
        return new DO.Task
        {
            ID = boTask.ID,
            Description = boTask.Description,
            Nickname = boTask.Nickname,
            Milestone = false,
            Production = boTask.Production,
            EstimatedStartDate = boTask.EstimatedStartDate,
            AcualStartNate = boTask.AcualStartNate,
            EstimatedEndDate = boTask.EstimatedEndDate,
            deadline = boTask.deadline,
            AcualEndNate = boTask.AcualEndNate,
            Product = boTask.Product,
            Remaeks = boTask.Remaeks,
            IDEngineer = boTask.EngineerIdName!.ID,
            Difficulty = (DO.EngineerLevelEnum)boTask.Difficulty,
        };
    }

    private TasksEngineer findEngineer(int id)
    {
        try
        {
            return new BO.TasksEngineer { ID = id, Name = _dal.Engineer.Read(id)!.Name };
        }
        catch
        {
            throw new NotImplementedException();
        }
    }

    private List<int> FindIdList(int TaskId)
    {
        return (from doDependence in _dal.Dependence.ReadAll()
                where TaskId == doDependence.IDTask
                select doDependence.IDPreviousTask)
        .ToList();
    }
    private List<TaskInList> FindTaskList(int TaskId)
    {

        return FindIdList(TaskId)
            .Select(id => _dal.Task.Read(id)!)
            .Select(task => new TaskInList
            {
                ID = task.ID,
                Nickname = task.Nickname!,
                Description = task.Description!,
                Status = FindStatus(task)
            })
            .ToList();
    }

    private BO.Status FindStatus(DO.Task doTask)
    {
        return (BO.Status)(doTask.EstimatedStartDate is null ? 0
                            : doTask.AcualStartNate is null ? 1
                            : doTask.AcualEndNate is null ? 2
                            : 3);
    }

    private BO.MilestoneIdNickname findMilestone(int TaskId)
    {
        BO.MilestoneIdNickname? milestone = (from id in FindIdList(TaskId)
                                             let task = _dal.Task.Read(id)!
                                             where task.Milestone == true
                                             select new BO.MilestoneIdNickname
                                             {
                                                 ID = id!,
                                                 NickName = task.Nickname!
                                             }).FirstOrDefault();
        return milestone;
    }
}