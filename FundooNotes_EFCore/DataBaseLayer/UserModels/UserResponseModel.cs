using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.UserModels
{
    public class UserResponseModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
