
using DalApi;
using DO;

namespace Dal;

internal class DependenceImplementation : IDependence
{
    /// <summary>
    /// The function creates a new dependence and returns its serial number
    /// </summary>
    public int Create(Dependence item)
    {
        List<Dependence> ls = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        int newId = Config.NextDependnceId;
        Dependence dependence = item with { ID = newId };
        ls.Add(dependence);
        XMLTools.SaveListToXMLSerializer(ls, "dependences");
        return newId;
    }

    /// <summary>
    /// The function delete a dependence 
    /// </summary>
    public void Delete(int id)
    {
        List<Dependence> ls = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        Dependence dependence = ls.Where(item => item.ID == id).First() ??
           throw new DalDoesNotExistException($"Dependence with ID {id} does not exist");
        ls.Remove(dependence);
        XMLTools.SaveListToXMLSerializer(ls, "dependences");
    }

    /// <summary>
    /// The function reads a dependence according to the id and returns him
    /// </summary>
    public Dependence? Read(int id)
    {
        List<Dependence> ls = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        Dependence dependence = ls.Where(s => s!.ID == id).First() ??
            throw new DalDoesNotExistException($"Dependence with ID {id} does not exist");
        return dependence;
    }

    /// <summary>
    /// The function reads a dependence and returns him
    /// </summary>
    public Dependence? Read(Func<Dependence, bool> filter)
    {
        List<Dependence> ls = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        Dependence dependence = ls.Where(filter).First() ??
            throw new DalDoesNotExistException($"Does not exist");
        return dependence;
    }

    /// <summary>
    /// The function read all the dependences and returns them
    /// </summary>
    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        List<Dependence> ls = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        if (filter == null)
            return ls.Select(item => item);
        else
            return ls.Where(filter);
    }

    /// <summary>
    /// The function updates the ditals of a dependence
    /// </summary>
    public void Update(Dependence item)
    {
        List<Dependence> ls = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        Dependence dependence = ls.Where(item1 => item1.ID == item.ID).First() ??
           throw new DalDoesNotExistException($"Dependence with ID {item.ID} does not exist");
        ls.Remove(dependence);
        ls.Add(item);
        XMLTools.SaveListToXMLSerializer(ls, "dependences");
    }
}
