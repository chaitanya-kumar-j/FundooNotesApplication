using System;
using System.Collections.Generic;
using System.Text;
using UserDataCommonLayer.Models;

namespace UserDataBusinessLogicLayer.Interfaces
{
    public interface IUserDataOperations
    {
        List<User> GetAllUsers();
        User RegisterUser(NewUser newUser);
        Response Login(Login loginDetails);
    }
}
