using System;


namespace FilozopLab04.UsersListProject.Exceptions
{
    class DateIsInFutureException : Exception
    {
        public DateIsInFutureException() : base("You are not born yet") { }
        public DateIsInFutureException(string message) : base(message) { }
    }
}