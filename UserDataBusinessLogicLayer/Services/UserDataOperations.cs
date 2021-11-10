using System;
using System.Collections.Generic;
using System.Text;
using UserDataBusinessLogicLayer.Interfaces;
using UserDataCommonLayer.Models;
using UserDataRepositoryLayer.Interfaces;

namespace UserDataBusinessLogicLayer.Services
{
    public class UserDataOperations : IUserDataOperations
    {
        private IUserDataAccess _userDataAccess;
        public UserDataOperations(IUserDataAccess userDataAccess)
        {
            this._userDataAccess = userDataAccess;
        }

        

        public List<User> GetAllUsers()
        {
            try
            {
                return this._userDataAccess.GetAllUsers();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public User RegisterUser(NewUser newUser)
        {
            try
            {
                return this._userDataAccess.RegisterUser(newUser);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public Response Login(Login loginDetails)
        {
            try
            {
                return this._userDataAccess.Login(loginDetails);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
