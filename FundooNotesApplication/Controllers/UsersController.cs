using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserDataBusinessLogicLayer.Interfaces;
using UserDataCommonLayer.Models;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserDataOperations _userDataOperations;
        public UsersController(IUserDataOperations userDataOperations)
        {
            this._userDataOperations = userDataOperations;
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            try
            {
                List<User> usersData = this._userDataOperations.GetAllUsers();
                return this.Ok(new { Success = true, Message = "Get request is successful", Data = usersData });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }
        [Route("Register")]
        [HttpPost]
        public ActionResult RegisterUser(NewUser newUser)
        {
            try
            {
                User usersData = this._userDataOperations.RegisterUser(newUser);
                return this.Ok(new { Success = true, Message = "User registration is successful", Data = usersData });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }
    }
}
