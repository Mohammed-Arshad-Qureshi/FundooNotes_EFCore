using BusinessLayer.Interface;
using DataBaseLayer.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLogger.Interface;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FundooNotes_EFCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly FundooContext fundooContext;
        private readonly IUserBL userBL;
        private readonly ILoggerManager logger;

        public UserController(FundooContext fundooContext, IUserBL userBL, ILoggerManager logger)
        {
            this.fundooContext = fundooContext;
            this.userBL = userBL;
            this.logger = logger;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserModel userModel)
        {
            try
            {
                this.logger.LogInfo($"User Registerd Email : {userModel.Email}");
                this.userBL.AddUser(userModel);
                return this.Ok(new { success = true, message = "User Created Successfully" });
            }
            catch (Exception ex)
            {
                this.logger.LogError($"User Registration Failed : {userModel.Email}");
                throw ex;
            }
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> getusers = new List<User>();
                getusers = this.userBL.GetAllUsers();
                return Ok(new { success = false, message = "GetAll users Fetch Successfully" ,data=getusers });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("LoginUser")]
        public IActionResult LoginUser(UserLoginModel userModel)
        {
            try
            {
                this.logger.LogInfo($"User cred Email : {userModel.Email}");
                string token = this.userBL.LoginUser(userModel);
                if (token == null)
                {
                    return this.BadRequest(new { success = false, message = "Enter Valid Email and Password" });
                }

                return this.Ok(new { success = true, message = "User Login Successfully", data = token });
            }
            catch (Exception ex)
            {
                this.logger.LogError($"User cred Failed : {userModel.Email}");
                throw ex;
            }
        }

        [HttpPost("ForgetUser/{email}")]
        public IActionResult ForgetUser(string email)
        {
            try
            {
                bool isExist = this.userBL.ForgetPasswordUser(email);
                if (isExist) return Ok(new { success = true, message = $"Reset Link sent to Eamil : {email}" });
                else return BadRequest(new { success = false, message = $"No user Exist with Email : {email}" });
            }
            catch (Exception ex)
            {
                this.logger.LogError($"User cred Failed : {email}");
                throw ex;
            }
        }
    }
}
