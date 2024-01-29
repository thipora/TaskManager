namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// The function creates a new task and returns its serial number
    /// </summary>
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task task = item with { ID = newId };
        DataSource.Tasks.Add(task);
        return newId;
    }


    /// <summary>
    /// The function delete a task 
    /// </summary>
    public void Delete(int id)
    {
        Task task = DataSource.Tasks.Where(item => item.ID == id).First() ??
            throw new DalDoesNotExistException($"Task with ID {id} does not exist");
        DataSource.Tasks.Remove(task);
    }
    /// <summary>
    /// The function read a task and returns him
    /// </summary>
    public Task? Read(Func<Task, bool> filter)
    {
        Task task = DataSource.Tasks.Where(filter).First() ??
            throw new DalDoesNotExistException($"Does not exist");
        return task;
    }
    /// <summary>
    /// The function reads a task according to the id and returns him
    /// </summary>
    public Task? Read(int id)
    {
        Task taskFind = DataSource.Tasks.Where(s => s!.ID == id).First() ??
            throw new DalDoesNotExistException($"Task with ID {id} does not exist");
        return taskFind;
    }
    /// <summary>
    /// The function read all the engineers and returns them
    /// </summary>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
    }
    /// <summary>
    /// The function updates the ditals of a task
    /// </summary>
    public void Update(Task item)
    {
        Task task = DataSource.Tasks.Where(item1 => item1.ID == item.ID).First() ??
           throw new DalDoesNotExistException($"Task whith ID {item.ID} does not exist");
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(item);
    }
}