using DataBaseLayer.UserModels;
using RepositoryLayer.Services.Entities;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public void AddUser(UserModel userModel);

        public List<User> GetAllUsers();

        public string LoginUser(UserLoginModel loginUser);
    }
}
