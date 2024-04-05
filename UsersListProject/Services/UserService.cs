using FilozopLab04.UsersListProject.Models;
using FilozopLab04.UsersListProject.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FilozopLab04.UsersListProject.Services
{
    internal class UserService
    {
        private static FileRepository Repository = new();

        public async Task AddOrUpdateUser(EditOrAddUser enterPerson)
        {
            Thread.Sleep(1000);
            if (String.IsNullOrWhiteSpace(enterPerson.FirstName) || String.IsNullOrWhiteSpace(enterPerson.LastName)
                || String.IsNullOrEmpty(enterPerson.Email) || enterPerson.DateOfBirth == null)
                throw new ArgumentException("First name, Last name, Email or Date of birth is Empty");
            DBUser dbUser = new DBUser(enterPerson.Guid, enterPerson.FirstName, enterPerson.LastName, enterPerson.Email, enterPerson.DateOfBirth.Value);
            await Repository.AddOrUpdate(dbUser);
        }

        public List<Person> GetAllPersons()
        {
            var res = new List<Person>();
            foreach (var user in Repository.GetAll())
            {
                res.Add(new Person(user.Guid, user.FirstName, user.LastName, user.Email, user.DateOfBirth));
            }
            return res;
        }

        public void DeleteUser(Person user)
        {
            Repository.Delete(user.Guid);
        }
    }
}