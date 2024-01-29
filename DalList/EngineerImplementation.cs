namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// The function creates a new engineer and returns his ID
    /// </summary>
    public int Create(Engineer item)
    {
        foreach (Engineer engineer in DataSource.Engineers)
            if (engineer.ID == item.ID)
                throw new DalDoesNotExistException($"Engineer with ID {item.ID} does not exist");
        DataSource.Engineers.Add(item);
        return item.ID;
    }
    /// <summary>
    /// The function delete an engineer 
    /// </summary>
    public void Delete(int id)
    {
        Engineer engineer = DataSource.Engineers.Where(item => item.ID == id).First() ??
           throw new DalDoesNotExistException($"Engineer with ID {id} does not exist");
        DataSource.Engineers.Remove(engineer);
    }
    /// <summary>
    /// The function read an engineer and returns him
    /// </summary>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        Engineer engineer = DataSource.Engineers.Where(filter).First() ??
            throw new DalDoesNotExistException($"Does not exist");
        return engineer;
    }
    /// <summary>
    /// The function reads a engineer according to the id and returns him
    /// </summary>
    public Engineer? Read(int id)
    {
        Engineer engineerFind = DataSource.Engineers.Where(s => s!.ID == id).First() ??
                       throw new DalDoesNotExistException($"Engineer with ID {id} does not exist");
        return engineerFind;
    }
    /// <summary>
    /// The function read all the engineers and returns them
    /// </summary>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }
    /// <summary>
    /// The function updates the ditals of an engineer
    /// </summary>
    public void Update(Engineer item)
    {
        Engineer engineer = DataSource.Engineers.Where(item1 => item1.ID == item.ID).First() ??
            throw new DalDoesNotExistException($"Engineer with ID {item.ID} does not exist");
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(item);
    }
}