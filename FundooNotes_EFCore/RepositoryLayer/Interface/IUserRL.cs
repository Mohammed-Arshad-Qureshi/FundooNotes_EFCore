using DataBaseLayer.UserModels;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public void AddUser(UserModel userModel);

        public List<User> GetAllUsers();

        public string LoginUser(UserLoginModel loginUser);
    }
}
