using System;
using System.Collections.Generic;

namespace Template.Authentication.Model
{
    public class User
    {
        public Guid UserID { get; set; }
        public String UserName { get; set; }
        public String HashedPassword { get; set; }
        public String Salt { get; set; }
        public List<Roll> Rolls { get; set; }
    }
}
