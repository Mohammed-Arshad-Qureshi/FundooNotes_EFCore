using BusinessLayer.Interface;
using DataBaseLayer.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLogger.Interface;
using RepositoryLayer.Services;
using System;

namespace FundooNotes_EFCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly FundooContext fundooContext;
        private readonly IUserBL userBl;
        private readonly ILoggerManager logger;

        public UserController(FundooContext fundooContext, IUserBL userBL, ILoggerManager logger)
        {
            this.fundooContext = fundooContext;
            this.userBl = userBL;
            this.logger = logger;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserModel userModel)
        {
            try
            {
                this.logger.LogInfo($"User Registerd Email : {userModel.Email}");
                this.userBl.AddUser(userModel);
                return this.Ok(new { success = true, message = "User Created Successfully" });
            }
            catch (Exception ex)
            {
                this.logger.LogError($"User Registration Failed : {userModel.Email}");
                throw ex;
            }
        }
    }
}
