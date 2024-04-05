using FilozopLab04.UsersListProject.Managers;
using FilozopLab04.UsersListProject.Exceptions;
using FilozopLab04.UsersListProject.Services;
using FilozopLab04.UsersListProject.Navigation;
using FilozopLab04.UsersListProject.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using FilozopLab04.UsersListProject.Models;


namespace FilozopLab04.UsersListProject.ViewModels
{
    internal class EditOrAddUserViewModel : INavigatable<MainNavigationTypes>, INotifyPropertyChanged
    {
        private RelayCommand<object> _saveCommand;
        private RelayCommand<object> _cancelCommand;
        private Action _goToUsersList;

        public EditOrAddUserViewModel(Action goToUsersList)
        {
            _goToUsersList = goToUsersList;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FirstName
        {
            get
            {
                return ApplicationManager.CurrentEnterUser.FirstName;
            }
            set
            {
                ApplicationManager.CurrentEnterUser.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get
            {
                return ApplicationManager.CurrentEnterUser.LastName;
            }
            set
            {
                ApplicationManager.CurrentEnterUser.LastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get
            {
                return ApplicationManager.CurrentEnterUser.Email;
            }
            set
            {
                ApplicationManager.CurrentEnterUser.Email = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DateOfBirth
        {
            get
            {
                return ApplicationManager.CurrentEnterUser.DateOfBirth;
            }
            set
            {
                ApplicationManager.CurrentEnterUser.DateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand<object>(_ => Save(), CanExecute);
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => _goToUsersList.Invoke());
            }
        }

        public MainNavigationTypes ViewType
        {
            get
            {
                return MainNavigationTypes.EnterUser;
            }
        }

        private async void Save()
        {
            var userService = new UserService();

            try
            {
                LoaderManager.Instance.ShowLoader();
                await Task.Run(() => userService.AddOrUpdateUser(ApplicationManager.CurrentEnterUser));

                ApplicationManager.CurrentPersonList = new ObservableCollection<Person>(userService.GetAllPersons());
                _goToUsersList.Invoke();
            }
            catch (WrongEmailException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            catch (DateIsTooOldException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            catch (DateIsInFutureException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                LoaderManager.Instance.HideLoader();
            }
        }



        private bool CanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(ApplicationManager.CurrentEnterUser.FirstName)
                && !String.IsNullOrWhiteSpace(ApplicationManager.CurrentEnterUser.LastName)
                && !String.IsNullOrEmpty(ApplicationManager.CurrentEnterUser.Email)
                && DateOfBirth != null;
        }

    }
}