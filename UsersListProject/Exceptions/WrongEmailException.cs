using System;

namespace FilozopLab04.UsersListProject.Exceptions
{
    class WrongEmailException : Exception
    {
        public WrongEmailException() : base("Email is invalid") { }
        public WrongEmailException(string message) : base(message) { }
    }

}
