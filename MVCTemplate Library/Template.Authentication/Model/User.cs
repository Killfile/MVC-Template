using System;
using AutoMapper;
using Template.PersistanceContract.Membership;

namespace Template.Authentication.Model
{
    public class User
    {
       

        public User()
        {
        }

        public Guid UserID { get; set; }
        public String UserName { get; set; }
        public String HashedPassword { get; set; }
        public String Salt { get; set; }
    }
}
