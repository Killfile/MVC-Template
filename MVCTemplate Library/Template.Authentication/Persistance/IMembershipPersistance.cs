using System;
using System.Collections.Generic;
using Template.Authentication.Model;

namespace Template.PersistanceContract.Membership
{
    public interface IMembershipPersistance
    {
        void Insert(User user);
        void Update(User user);
        User GetByUsername(String username);
        User GetByID(Guid userID);
        List<User> GetAll();

    }
}