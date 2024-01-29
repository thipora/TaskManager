namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
internal class DependenceImplementation : IDependence
{
    /// <summary>
    /// The function creates a new dependence and returns its serial number
    /// </summary>
    public int Create(Dependence item)
    {
        int newId = DataSource.Config.NextDependenceId;
        Dependence dependence = item with { ID = newId };
        DataSource.Dependences.Add(dependence);
        return newId;
    }
    /// <summary>
    /// The function delete a dependence 
    /// </summary>
    public void Delete(int id)
    {
        Dependence dependence = DataSource.Dependences.Where(item => item.ID == id).First() ??
           throw new DalDoesNotExistException($"Dependence with ID {id} does not exist");
        DataSource.Dependences.Remove(dependence);
    }
    /// <summary>
    /// The function reads a dependence according to the id and returns him
    /// </summary>
    public Dependence? Read(int id)
    {
        Dependence dependenceFind = DataSource.Dependences.Where(s => s!.ID == id).First() ??
            throw new DalDoesNotExistException($"Dependence with ID {id} does not exist");
        return dependenceFind;
    }
    /// <summary>
    /// The function reads a dependence and returns him
    /// </summary>
    public Dependence? Read(Func<Dependence, bool> filter)
    {
        Dependence dependence = DataSource.Dependences.Where(filter).First() ??
            throw new DalDoesNotExistException($"Does not exist");
        return dependence;
    }
    /// <summary>
    /// The function read all the dependences and returns them
    /// </summary>
    public IEnumerable<Dependence?> ReadAll(Func<Dependence?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Dependences.Select(item => item);
        else
            return DataSource.Dependences.Where(filter);
    }

    /// <summary>
    /// The function updates the ditals of a dependence
    /// </summary>
    public void Update(Dependence item)
    {
        Dependence dependence = DataSource.Dependences.Where(item1 => item1.ID == item.ID).First() ??
           throw new DalDoesNotExistException($"Dependence with ID {item.ID} does not exist");
        DataSource.Dependences.Remove(dependence);
        DataSource.Dependences.Add(item);
    }
}
