namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml.Linq;
internal class TaskImplementation : ITask
{
    /// <summary>
    /// The function creates a new task and returns its serial number
    /// </summary>
    public int Create(DO.Task item)
    {
        List<DO.Task> ls = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        int newId = Config.NextTaskId;
        DO.Task task = item with { ID = newId };
        ls.Add(task);
        XMLTools.SaveListToXMLSerializer(ls, "tasks");
        return newId;
    }


    /// <summary>
    /// The function delete a task 
    /// </summary>
    public void Delete(int id)
    {
        List<DO.Task> ls = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        DO.Task task = ls.Where(s => s.ID == id).First() ??
           throw new DalDoesNotExistException($"Task with ID {id} does not exist");
        ls.Remove(task);
        XMLTools.SaveListToXMLSerializer(ls, "tasks");
    }
    /// <summary>
    /// The function read a task and returns him
    /// </summary>
    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> ls = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        DO.Task task = ls.Where(filter).First() ??
            throw new DalDoesNotExistException($"Does not exist");
        return task;
    }
    /// <summary>
    /// The function reads a task according to the id and returns him
    /// </summary>
    public DO.Task? Read(int id)
    {
        List<DO.Task> ls = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        DO.Task task = ls.Where(s => s!.ID == id).First() ??
            throw new DalDoesNotExistException($"Task with ID {id} does not exist");
        return task;
    }
    /// <summary>
    /// The function read all the engineers and returns them
    /// </summary>
    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<DO.Task> ls = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        if (filter == null)
            return ls.Select(item => item);
        else
            return ls.Where(filter);
    }
    /// <summary>
    /// The function updates the ditals of a task
    /// </summary>
    public void Update(DO.Task item)
    {
        List<DO.Task> ls = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        DO.Task task = ls.Where(item1 => item1.ID == item.ID).First() ??
           throw new DalDoesNotExistException($"Task with ID {item.ID} does not exist");
        ls.Remove(task);
        ls.Add(item);
        XMLTools.SaveListToXMLSerializer(ls, "tasks");
    }
}