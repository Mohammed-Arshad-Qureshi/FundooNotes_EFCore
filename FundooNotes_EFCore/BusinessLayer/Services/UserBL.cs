using BusinessLayer.Interface;
using DataBaseLayer.UserModels;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public void AddUser(UserModel userModel)
        {
            try
            {
                this.userRL.AddUser(userModel);
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
                return this.userRL.GetAllUsers();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
