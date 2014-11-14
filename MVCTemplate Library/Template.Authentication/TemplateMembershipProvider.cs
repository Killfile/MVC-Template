using System;
using AutoMapper;
using Template.Authentication.Model;
using Template.Authentication.PasswordHashing;
using Template.PersistanceContract.Membership;

namespace Template.Authentication
{
    public class TemplateMembershipProvider
    {
        private readonly IMembershipPersistance _membershipPersistance;
        private readonly IPasswordHashing _hashingEngine;

        public TemplateMembershipProvider(IMembershipPersistance membershipPersistance, IPasswordHashing hashingEngine)
        {
            _membershipPersistance = membershipPersistance;
            _hashingEngine = hashingEngine;
        }

        public User CreateUser(string userName, string password)
        {
            string mySalt = _hashingEngine.GetSalt();
            string myHash = _hashingEngine.Hash(password, mySalt);

            User toCreate = new User()
            {
                UserID = Guid.NewGuid(),
                UserName = userName,
                Salt = mySalt,
                HashedPassword = myHash
            };

            
            _membershipPersistance.Insert(toCreate);

            return toCreate;
            //bool doesPasswordMatch = BCrypt.CheckPassword(myPassword, myHash);
        }

        public bool ValidateUser(string userName, string password)
        {
            User user = _membershipPersistance.GetByUsername(userName);
            if (user == null)
                return false;
            return _hashingEngine.Verify(password, user.HashedPassword, "");
        }

        public User GetUser(string userName)
        {
            User returnValue = _membershipPersistance.GetByUsername(userName);
            
            return returnValue;
        }
    }
}
