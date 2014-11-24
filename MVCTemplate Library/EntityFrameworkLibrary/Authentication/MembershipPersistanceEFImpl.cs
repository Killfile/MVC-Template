using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ContosoUniversity.DAL;
using Template.Authentication.Model;
using Template.MappingContract;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Membership;
using Template.PersistanceContract.Membership;

namespace Template.Persistance.EntityFrameworkImpl.Authentication
{
    public class MembershipPersistanceEFImpl : IMembershipPersistance 
    {
        private readonly TemplateDBContext _context;

        private readonly MappingBaseline _mappingCreator;

        public MembershipPersistanceEFImpl(MappingBaseline mappingCreator, TemplateDBContext context)
        {
            _mappingCreator = mappingCreator;
            _context = context;
        }

        public void Insert(User user)
        {
            EFMembership efUser = _mappingCreator.Map<User,EFMembership>(user);
            _context.Memberships.Add(efUser);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            EFMembership efUser = _mappingCreator.Map<User,EFMembership>(user);
            _context.Entry(efUser).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public User GetByUsername(string username)
        {
            IQueryable<EFMembership> users = _context.Memberships.Where(u => u.UserName == username);
            if (!users.Any())
                return null;

            return _mappingCreator.Map<EFMembership,User>(users.ToList()[0]);
        }

        public User GetByID(Guid userID)
        {
            EFMembership user = _context.Memberships.Find(userID);
            if (user == null)
                return null;
            return _mappingCreator.Map<EFMembership,User>(user);
        }

        public List<User> GetAll()
        {
            List<EFMembership> allUsers = _context.Memberships.ToList();
            List<User> mappedUsers = _mappingCreator.Map<List<EFMembership>, List<User>>(allUsers);
            return mappedUsers;
        }

       
    }
}
