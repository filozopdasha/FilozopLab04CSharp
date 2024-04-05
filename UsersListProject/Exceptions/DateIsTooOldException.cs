using System;

namespace FilozopLab04.UsersListProject.Exceptions
{
    class DateIsTooOldException : Exception
    {
        public DateIsTooOldException() : base("You are probably dead") { }
        public DateIsTooOldException(string message) : base(message) { }


    }
}