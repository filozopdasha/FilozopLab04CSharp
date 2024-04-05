using FilozopLab04.UsersListProject.Models;
using System.Collections.ObjectModel;


namespace FilozopLab04.UsersListProject.Managers
{
    internal class ApplicationManager
    {
        public static EditOrAddUser CurrentEnterUser { get; set; }
        public static ObservableCollection<Person> CurrentPersonList { get; set; }
    }
}