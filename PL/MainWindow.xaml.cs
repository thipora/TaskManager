
using System.Windows;
using PL.Engineer;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void btnEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }
}
