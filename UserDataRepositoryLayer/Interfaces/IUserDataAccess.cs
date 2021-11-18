using System;
using System.Collections.Generic;
using System.Text;
using UserDataCommonLayer.Models;

namespace UserDataRepositoryLayer.Interfaces
{
    public interface IUserDataAccess
    {
        List<User> GetAllUsers();
        User RegisterUser(NewUser newUser);
        Response Login(Login loginDetails);
        Response ResetPassword(int userId, Reset resetDetails);
        void ForgotPassword(string email);
    }
}
