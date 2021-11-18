using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserDataBusinessLogicLayer.Interfaces;
using UserDataCommonLayer.Models;
using UserDataRepositoryLayer.Interfaces;
using UserDataRepositoryLayer.Services;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserDataOperations _userDataOperations;
        private IConfiguration _configuration;
        public readonly IEmailService _emailService;


        public UsersController(IUserDataOperations userDataOperations, IConfiguration configuration, IEmailService emailService)
        {
            this._userDataOperations = userDataOperations;
            this._configuration = configuration;
            this._emailService = emailService;
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
        [Route("Login")]
        [HttpPost]
        public ActionResult Login(Login loginDetails)
        {
            try
            {
                
                Response usersData = this._userDataOperations.Login(loginDetails);
                string token = new JwtService(_configuration).GenerateSecurityToken(usersData);
                return this.Ok(new { Success = true, Message = "User Login is successful", Data = usersData, Token = token });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public ActionResult ResetPassword(Reset resetDetails)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Response response = this._userDataOperations.ResetPassword(userId, resetDetails);
                return this.Ok(new { Success = true, Message = "Password reset is successful", Data = response });
                
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }
        
        [HttpPut("ForgetPassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                _userDataOperations.ForgotPassword(email);
                return this.Ok(new { Success = true, Message = "Reset link sent successfully"});

            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }

    }
}
