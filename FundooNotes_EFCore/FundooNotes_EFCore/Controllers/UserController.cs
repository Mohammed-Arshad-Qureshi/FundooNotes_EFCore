using BusinessLayer.Interface;
using DataBaseLayer.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(FundooContext fundooContext, IUserBL userBL)
        {
            this.fundooContext = fundooContext;
            this.userBl = userBL;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserModel userModel)
        {
            try
            {
                this.userBl.AddUser(userModel);
                return this.Ok(new { success = true, message = "User Created Successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
