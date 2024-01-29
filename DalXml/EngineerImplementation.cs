
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Emit;
using System.Xml.Linq;
internal class EngineerImplementation : IEngineer
{
    const string s_engineers = "engineers";
    /// <summary>
    ///  Gets an engineer.
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    static Engineer? GetEngineer(XElement? e)
    {
        if (e == null)
            return null;
        return e.ToIntNullable("ID") is null ? null : new Engineer()
        {
            ID = (int)e.Element("ID")!,
            Name = (string)e.Element("Name")!,
            Email = (string)e.Element("Email")!,
            EngineerLevel = (EngineerLevelEnum)XMLTools.ToEnumNullable<EngineerLevelEnum>(e, "EngineerLevel")!,
            PriceOfHour = (float)e.Element("PriceOfHour")!
        };
    }

    /// <summary>
    /// Creates an engineer element.
    /// </summary>
    /// <param name="engineer"></param>
    /// <returns></returns>
    static IEnumerable<XElement> CreateEngineerElement(Engineer engineer)
    {
        yield return new XElement("ID", engineer.ID);
        if (engineer.Name is not null)
            yield return new XElement("Name", engineer.Name);
        if (engineer.Email is not null)
            yield return new XElement("Email", engineer.Email);
        //if (engineer.EngineerLevel != null)
        yield return new XElement("EngineerLevel", engineer.EngineerLevel);
        yield return new XElement("PriceOfHour", engineer.PriceOfHour);
    }
    /// <summary>
    /// finds an engineer by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DalDoesNotExistException"></exception>

    public Engineer? Read(int id)
    {
        XElement? engineer = XMLTools.LoadListFromXMLElement(s_engineers)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == id);
        if (engineer == null)
        {
            throw new DalDoesNotExistException($"Does not exist");
        }
        return GetEngineer(engineer);
    }

    /// <summary>
    /// finds an engineer by specific attribute using filter.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    /// 

    //public Engineer? Read(Func<Engineer, bool> filter) =>
    //    XMLTools.LoadListFromXMLElement(s_engineers).Elements().Select(s => GetEngineer(s)).Where(filter!).FirstOrDefault() ?? null;

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        var engineer = XMLTools.LoadListFromXMLElement(s_engineers).Elements().Select(s => GetEngineer(s)).Where(filter!).FirstOrDefault();
        if (engineer == null)
        {
            throw new DalDoesNotExistException($"Engineer with such filter does not exist");
        }
        return engineer;
    }

    /// <summary>
    /// returns all engineers
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null) =>
        filter is null
        ? XMLTools.LoadListFromXMLElement(s_engineers).Elements().Select(s => GetEngineer(s))
        : XMLTools.LoadListFromXMLElement(s_engineers).Elements().Select(s => GetEngineer(s)).Where(filter!);

    /// <summary>
    /// creates a new engineer.
    /// </summary>
    /// <param name="engineer"></param>
    /// <returns></returns>
    /// <exception cref="DalAlreadyExistsException"></exception>
    public int Create(Engineer engineer)
    {
        XElement engineersRootElem = XMLTools.LoadListFromXMLElement(s_engineers);
        if (XMLTools.LoadListFromXMLElement(s_engineers)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == engineer.ID) is not null)
            throw new DalAlreadyExistsException("id already exist");
        engineersRootElem.Add(new XElement("Engineer", CreateEngineerElement(engineer)));
        XMLTools.SaveListToXMLElement(engineersRootElem, s_engineers);

        return engineer.ID;
    }

    /// <summary>
    /// deletes an engineer from data using a list.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        XElement engineersRootElem = XMLTools.LoadListFromXMLElement(s_engineers);

        (engineersRootElem.Elements()
            // fix to: throw new DalMissingIdException(id);
            .FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new DalDoesNotExistException("missing id"))
            .Remove();

        XMLTools.SaveListToXMLElement(engineersRootElem, s_engineers);
    }

    /// <summary>
    /// updates an engineer
    /// </summary>
    /// <param name="engineer"></param>
    public void Update(Engineer engineer)
    {
        Delete(engineer.ID);
        Create(engineer);
    }
}


//using DalApi;
//using DO;

//namespace Dal;

//internal class EngineerImplementation : IEngineer
//{
//    /// <summary>
//    /// The function creates a new engineer and returns his ID
//    /// </summary>
//    public int Create(Engineer item)
//    {
//        List<Engineer> ls = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        foreach (Engineer engineer in ls)
//            if (engineer.ID == item.ID)
//                throw new DalDoesNotExistException($"Engineer with ID {item.ID} exist");
//        ls.Add(item);
//        XMLTools.SaveListToXMLSerializer(ls, "engineers");
//        return item.ID;
//    }

//    /// <summary>
//    /// The function delete an engineer 
//    /// </summary>
//    public void Delete(int id)
//    {
//        List<Engineer> ls = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        Engineer engineer = ls.Where(item => item.ID == id).First() ??
//           throw new DalDoesNotExistException($"Engineer with ID {id} does not exist");
//        ls.Remove(engineer);
//        XMLTools.SaveListToXMLSerializer(ls, "engineers");
//    }

//    /// <summary>
//    /// The function read an engineer and returns him
//    /// </summary>
//    public Engineer? Read(Func<Engineer, bool> filter)
//    {
//        List<Engineer> ls = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        Engineer engineer = ls.Where(filter).First() ??
//            throw new DalDoesNotExistException($"Does not exist");
//        return engineer;
//    }

//    /// <summary>
//    /// The function reads a engineer according to the id and returns him
//    /// </summary>
//    public Engineer? Read(int id)
//    {
//        List<Engineer> ls = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        Engineer engineerFind = ls.Where(s => s!.ID == id).First() ??
//                       throw new DalDoesNotExistException($"Engineer with ID {id} does not exist");
//        return engineerFind;
//    }

//    /// <summary>
//    /// The function read all the engineers and returns them
//    /// </summary>
//    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
//    {
//        List<Engineer> ls = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        if (filter == null)
//            return ls.Select(item => item);
//        else
//            return ls.Where(filter);
//    }

//    /// <summary>
//    /// The function updates the ditals of an engineer
//    /// </summary>
//    public void Update(Engineer item)
//    {
//        List<Engineer> ls = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        Engineer engineer = ls.Where(item1 => item1.ID == item.ID).First() ??
//            throw new DalDoesNotExistException($"Engineer with ID {item.ID} does not exist");
//        ls.Remove(engineer);
//        ls.Add(item);
//        XMLTools.SaveListToXMLSerializer(ls, "engineers");
//    }
//}