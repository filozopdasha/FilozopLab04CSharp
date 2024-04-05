using System;

namespace FilozopLab04.UsersListProject.Models
{
    internal class EditOrAddUser
    {
        private Guid _guid;
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime? _dateOfBirth;

        public EditOrAddUser()
        {
            _guid = Guid.NewGuid();
        }

        public EditOrAddUser(Guid guid, string firstName, string lastName, string email, DateTime? dateOfBirth)
        {
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
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
    }
}