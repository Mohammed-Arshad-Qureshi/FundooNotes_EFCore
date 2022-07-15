using DataBaseLayer.UserModels;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        public UserRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public void AddUser(UserModel userModel)
        {
            try
            {
                User user = new User();
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.Email = userModel.Email;
                user.password = userModel.Password;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                this.fundooContext.Users.Add(user);
                this.fundooContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return this.fundooContext.Users.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
