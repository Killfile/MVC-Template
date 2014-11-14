using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Template.Authentication.PasswordHashing
{
    public class BCryptHashImpl : IPasswordHashing
    {

        public string GetSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public string Hash(string plainText, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainText, salt);
        }

        public bool Verify(string plainText, string cryptText, string salt)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, cryptText);
        }
    }
}
