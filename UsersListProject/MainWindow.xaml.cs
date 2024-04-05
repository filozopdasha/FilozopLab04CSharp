using FilozopLab04.UsersListProject.ViewModels;
using System.Windows;


namespace FilozopLab04.UsersListProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}