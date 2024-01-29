
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BO;
using DalApi;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerListWindow()
        {
            InitializeComponent();
            var temp = EngineerToList(s_bl?.Engineer.ReadAll());
            //var temp = (from engineer in s_bl.Engineer.ReadAll()
            //            select new EngineerInList { Name = engineer.Name, Email = engineer.Email }).ToList();
            EngineerList = temp == null ? new() : new(temp);
        }

        public ObservableCollection<BO.EngineerInList> EngineerList
        {
            get { return (ObservableCollection<BO.EngineerInList>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
                DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.EngineerInList>), typeof(EngineerListWindow), new PropertyMetadata(null));
        public BO.EngineerLevelEnum LevelEngineer { get; set; } = BO.EngineerLevelEnum.None;
        //public BO.SemesterNames Semester { get; set; } = BO.SemesterNames.None;
        private void cbEngineerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            var temp = LevelEngineer == BO.EngineerLevelEnum.None ?
            EngineerToList(s_bl?.Engineer.ReadAll()) :
            EngineerToList(s_bl?.Engineer.ReadAll(LevelEngineer));//item => item.EngineerLevel == LevelEngineer
            EngineerList = temp == null ? new() : new(temp);
        }
        private IEnumerable<BO.EngineerInList> EngineerToList(IEnumerable<BO.Engineer> engineers)
        {
            return (from BO.Engineer engineer in engineers
                    select new EngineerInList { Name = engineer.Name, Email = engineer.Email }).ToList();
        }
    }
}


