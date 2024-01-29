namespace DalTest;

using Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Pipes;
using System.Threading.Tasks;
using System.Xml.Linq;


public static class Initialization
{
    private static IDal? s_dal;
    private static readonly Random s_rand = new();

    /// <summary>
    /// The function creates the array of Engineers
    /// </summary>
    private static void createEngineer()
    {

        string[] names = {
        "Alice","Bob","Charlie","David","Emma","Frank","Grace","Hannah","Isaac","Julia","Kevin","Linda","Michael","Nora","Oliver","Penny","Quincy","Rachel","Samuel","Tina","Ulysses", "Victoria","William", "Xander","Yvonne","Zachary","Sophia","Ethan","Ava","Liam", "Mia", "Noah","Olivia","Lucas","Charlotte","Elijah","Amelia","Mason","Harper","shani"
        };
        const int minID = 200000000;
        const int maxID = 400000000;

        foreach (string name in names)
        {
            int id = s_rand.Next(minID, maxID);
            string email = $"{names} + @gmail.com";
            EngineerLevelEnum engineerLevel = (EngineerLevelEnum)s_rand.Next(0, Enum.GetValues(typeof(EngineerLevelEnum)).Length);
            int priceOfHour = 40;
            switch (engineerLevel)
            {
                //AdvancedBeginner, Competent, Proficient, Expert
                case EngineerLevelEnum.AdvancedBeginner:
                    priceOfHour = 60;
                    break;
                case EngineerLevelEnum.Competent:
                    priceOfHour = 80;
                    break;
                case EngineerLevelEnum.Proficient:
                    priceOfHour = 100;
                    break;
                case EngineerLevelEnum.Expert:
                    priceOfHour = 130;
                    break;
            }
            Engineer new_engineer = new Engineer(id, name, email, engineerLevel, priceOfHour);
            s_dal.Engineer!.Create(new_engineer);
        }
    }

    /// <summary>
    /// The function creates the array of Tasks
    /// </summary>

    private static void createTasks()
    {
        DateTime start = DateTime.Today.AddYears(-1);
        int range = (DateTime.Today.Month - start.Month) + 12 * (DateTime.Today.Year - start.Year);
        List<DO.Task> newEngineers = (List<DO.Task>)s_dal.Engineer.ReadAll();

        for (int i = 0; i < 100; i++)
        {
            Random s_rand = new Random();
            DateTime Production = start.AddMonths(s_rand.Next(range));
            int longTime = s_rand.Next(30, 250);
            int IDEngineer = newEngineers[s_rand.Next(newEngineers.Count)].ID;
            EngineerLevelEnum Difficulty = (EngineerLevelEnum)new Random().Next(Enum.GetValues(typeof(EngineerLevelEnum)).Length);
            DO.Task new_task = new(0, null, null, false, Production, null, null, null, null, null, null, null, IDEngineer, Difficulty); ;
            s_dal!.Task.Create(new_task);
        }
    }
    /// <summary>
    /// The function creates the array of Dependence
    /// </summary>
    private static void createDependence()
    {
        int _next_task;
        int _prev_task;
        List<DO.Task> newTasks = (List<DO.Task>)s_dal.Task.ReadAll();
        foreach (var task in newTasks)
        {
            if (newTasks.FindIndex(_task => _task.ID == task.ID) == newTasks.Count - 4)
                break;
            _prev_task = task.ID;
            for (int i = 1; i < 4;)
            {
                _next_task = newTasks[newTasks.FindIndex(_task => _task.ID == task.ID) + i].ID;
                Dependence new_Dependence = new(0, _next_task, _prev_task);
                s_dal!.Dependence.Create(new_Dependence);
            }
        }
    }
    public static void Do() //stage 4
    {
        s_dal = DalApi.Factory.Get;
        createEngineer();
        createTasks();
        createDependence();
    }
}
