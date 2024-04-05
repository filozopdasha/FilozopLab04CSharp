using System.ComponentModel;
using System.Windows;

namespace FilozopLab04.UsersListProject.Managers
{
    interface ILoaderOwner : INotifyPropertyChanged
    {
        public bool IsEnabled
        {
            get;
            set;
        }

        public Visibility LoaderVisibility
        {
            get;
            set;
        }
    }
}