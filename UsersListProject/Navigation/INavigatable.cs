using System;

namespace FilozopLab04.UsersListProject.Navigation
{
    internal interface INavigatable<TObject> where TObject : Enum
    {
        TObject ViewType
        {
            get;
        }
    }
}