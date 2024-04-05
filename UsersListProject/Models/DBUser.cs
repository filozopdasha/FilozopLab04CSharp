using System;
using System.Text.RegularExpressions;
using FilozopLab04.UsersListProject.Exceptions;

namespace FilozopLab04.UsersListProject.Models
{
    internal class DBUser
    {
        private Guid _guid;
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateOfBirth;

        public DBUser(Guid guid, string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                throw new WrongEmailException(email);

            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (DateTime.Today < dateOfBirth.AddYears(age))
                --age;

            if (age < 0)
                throw new DateIsInFutureException(dateOfBirth.ToString("dd.MM.yyyy"));
            if (age > 135)
                throw new DateIsTooOldException(dateOfBirth.ToString("dd.MM.yyyy"));

            _guid = guid;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _dateOfBirth = dateOfBirth;
        }

        public Guid Guid
        {
            get { return _guid; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public string Email
        {
            get { return _email; }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
        }
    }
}