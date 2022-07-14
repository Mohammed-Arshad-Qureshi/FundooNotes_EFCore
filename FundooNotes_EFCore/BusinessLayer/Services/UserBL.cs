using BusinessLayer.Interface;
using DataBaseLayer.UserModels;
using RepositoryLayer.Interface;
using System;

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
    }
}
