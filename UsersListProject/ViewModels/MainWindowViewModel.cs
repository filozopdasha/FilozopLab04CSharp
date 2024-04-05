using FilozopLab04.UsersListProject.Managers;
using FilozopLab04.UsersListProject.Navigation;
using System.Windows;

namespace FilozopLab04.UsersListProject.ViewModels
{
    internal class MainWindowViewModel : BaseNavigatableViewModel<MainNavigationTypes>, ILoaderOwner
    {
        private bool _isEnabled = true;
        private Visibility _loaderVisibility = Visibility.Collapsed;

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public Visibility LoaderVisibility
        {
            get
            {
                return _loaderVisibility;
            }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            InitializeLoader();
            Navigate(MainNavigationTypes.PersonList);
        }

        private void InitializeLoader()
        {
            LoaderManager.Instance.Initialize(this);
        }

        protected override INavigatable<MainNavigationTypes> CreateViewModel(MainNavigationTypes type)
        {
            if (type == MainNavigationTypes.EnterUser)
            {
                return new EditOrAddUserViewModel(() => Navigate(MainNavigationTypes.PersonList));
            }
            else if (type == MainNavigationTypes.PersonList)
            {
                return new UsersListViewModel(() => Navigate(MainNavigationTypes.EnterUser));
            }
            else
            {
                return null;
            }
        }
    }
}
