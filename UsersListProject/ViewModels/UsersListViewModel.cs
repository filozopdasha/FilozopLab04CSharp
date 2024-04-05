using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FilozopLab04.UsersListProject.Managers;
using FilozopLab04.UsersListProject.Models;
using FilozopLab04.UsersListProject.Navigation;
using FilozopLab04.UsersListProject.Services;
using FilozopLab04.UsersListProject.Tools;

namespace FilozopLab04.UsersListProject.ViewModels
{
    internal class UsersListViewModel : INavigatable<MainNavigationTypes>, INotifyPropertyChanged
    {
        private readonly UserService _personService;
        private RelayCommand<string> _sortCommand;
        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _applyPersonCommand;
        private Action _goToEnterPerson;

        private string _sortedProperty;
        private bool _isAscendingOrder = true;
        private bool _filtersChanged = false;

        private string _firstNameFilter;
        public string FirstNameFilter
        {
            get => _firstNameFilter;
            set
            {
                _firstNameFilter = value;
                OnPropertyChanged();
                _filtersChanged = true;
            }
        }

        private string _surnameFilter;
        public string SurnameFilter
        {
            get => _surnameFilter;
            set
            {
                _surnameFilter = value;
                OnPropertyChanged();
                _filtersChanged = true;
            }
        }

        private string _emailFilter;
        public string EmailFilter
        {
            get => _emailFilter;
            set
            {
                _emailFilter = value;
                OnPropertyChanged();
                _filtersChanged = true;
            }
        }

        private DateTime? _dateOfBirthFilter;
        public DateTime? DateOfBirthFilter
        {
            get => _dateOfBirthFilter;
            set
            {
                _dateOfBirthFilter = value;
                OnPropertyChanged();
                _filtersChanged = true;
            }
        }

        private bool? _isAdultFilter;
        public bool? IsAdultFilter
        {
            get => _isAdultFilter;
            set
            {
                _isAdultFilter = value;
                OnPropertyChanged();
                _filtersChanged = true;
            }
        }

        private string _westernZodiacFilter;
        public string WesternZodiacFilter
        {
            get => _westernZodiacFilter;
            set
            {
                _westernZodiacFilter = value;
                OnPropertyChanged();
                _filtersChanged = true;
            }
        }

        private string _chineseZodiacFilter;
        public string ChineseZodiacFilter
        {
            get => _chineseZodiacFilter;
            set
            {
                _chineseZodiacFilter = value;
                OnPropertyChanged();
                _filtersChanged = true;
            }
        }

        private bool? _isBirthdayFilter;
        public bool? IsBirthdayFilter
        {
            get => _isBirthdayFilter;
            set
            {
                _isBirthdayFilter = value;
                OnPropertyChanged();
                _filtersChanged = true;
            }
        }

        public UsersListViewModel(Action goToEnterPerson)
        {
            _personService = new UserService();
            _goToEnterPerson = goToEnterPerson;
            RefreshPersonList();
        }

        public ObservableCollection<Person> PersonList
        {
            get => ApplicationManager.CurrentPersonList;
            set
            {
                ApplicationManager.CurrentPersonList = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<string> SortCommand => _sortCommand ??= new RelayCommand<string>(SortBy);

        public RelayCommand<object> AddPersonCommand => _addPersonCommand ??= new RelayCommand<object>(_ =>
        {
            ApplicationManager.CurrentEnterUser = new EditOrAddUser();
            _goToEnterPerson.Invoke();

        });



        public RelayCommand<object> DeletePersonCommand => _deletePersonCommand ??= new RelayCommand<object>(DeletePerson);

        public RelayCommand<object> EditPersonCommand => _editPersonCommand ??= new RelayCommand<object>(EditPerson);

        public string SortedProperty
        {
            get => _sortedProperty;
            set
            {
                _sortedProperty = value;
                OnPropertyChanged();
            }
        }

        public bool IsAscendingOrder
        {
            get => _isAscendingOrder;
            set
            {
                _isAscendingOrder = value;
                OnPropertyChanged();
            }
        }

        private void DeletePerson(object obj)
        {
            if (obj is not Person)
                return;

            _personService.DeleteUser((Person)obj);
            RefreshPersonList();
        }

        private void EditPerson(object obj)
        {
            if (obj is not Person user)
                return;

            ApplicationManager.CurrentEnterUser = new EditOrAddUser(user.Guid, user.FirstName, user.LastName, user.Email, user.DateOfBirth);
            _goToEnterPerson.Invoke();
        }

        private void RefreshPersonList()
        {
            PersonList = new ObservableCollection<Person>(_personService.GetAllPersons());
        }

        public void SortBy(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return;

            var property = typeof(Person).GetProperty(propertyName);
            if (property == null)
            {
                return;
            }

            if (SortedProperty == propertyName)
            {
                IsAscendingOrder = !IsAscendingOrder;
            }
            else
            {
                IsAscendingOrder = true;
            }

            SortedProperty = propertyName;

            if (IsAscendingOrder)
            {
                PersonList = new ObservableCollection<Person>(PersonList.OrderBy(u => property.GetValue(u)));
            }
            else
            {
                PersonList = new ObservableCollection<Person>(PersonList.OrderByDescending(u => property.GetValue(u)));
            }
        }

        public RelayCommand<object> ApplyPersonCommand => _applyPersonCommand ??= new RelayCommand<object>(_ => ApplyFilters());

        private void ApplyFilters()
        {
            if (!_filtersChanged)
                return;

            var filteredList = _personService.GetAllPersons()
                .Where(p =>
                    (string.IsNullOrWhiteSpace(_firstNameFilter) || p.FirstName.Contains(_firstNameFilter)) &&
                    (string.IsNullOrWhiteSpace(_surnameFilter) || p.LastName.Contains(_surnameFilter)) &&
                    (string.IsNullOrWhiteSpace(_emailFilter) || p.Email.Contains(_emailFilter)) &&
                    (!_dateOfBirthFilter.HasValue || p.DateOfBirth == _dateOfBirthFilter) &&
                    (!_isAdultFilter.HasValue || p.IsAdult == _isAdultFilter) &&
                    (string.IsNullOrWhiteSpace(_westernZodiacFilter) || p.WesternSign.Contains(_westernZodiacFilter)) &&
                    (string.IsNullOrWhiteSpace(_chineseZodiacFilter) || p.ChineseSign.Contains(_chineseZodiacFilter))
                );

            if (_isBirthdayFilter.HasValue && _isBirthdayFilter.Value)
            {
                filteredList = filteredList.Where(p => p.DateOfBirth.Month == DateTime.Now.Month && p.DateOfBirth.Day == DateTime.Now.Day);
            }

            PersonList = new ObservableCollection<Person>(filteredList);

            _filtersChanged = false;
        }

        public MainNavigationTypes ViewType => MainNavigationTypes.PersonList;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}