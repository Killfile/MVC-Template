using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using ContosoUniversity.DAL;
using Template.Authentication.Model;
using Template.MappingContract;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Membership;
using Template.PersistanceContract.Membership;


namespace Template.Persistance.EntityFrameworkImpl.PersistanceContractImpls.Membership
{
    public class MembershipEFImpl : IMembershipPersistance 
    {
        private TemplateDBContext _context;
        private IMappingContract _mappingContract;
        public void Insert(User user)
        {
            EFMembership EFUser = Mapper.Map<EFMembership>(user);
            _context.Memberships.Add(EFUser);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            EFMembership EFUser = Mapper.Map<EFMembership>(user);
            _context.Entry(EFUser).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public User GetByUsername(string username)
        {
            IQueryable<EFMembership> users = _context.Memberships.Where(u => u.UserName == username);
            if (!users.Any())
                return null;

            return Mapper.Map<User>(users.ToList()[0]);
        }

        public User GetByID(Guid userID)
        {
            EFMembership user = _context.Memberships.Find(userID);
            if (user == null)
                return null;
            return Mapper.Map<User>(user);
        }

        public List<User> GetAll()
        {
            List<EFMembership> allUsers = _context.Memberships.ToList();
            List<User> mappedUsers = Mapper.Map<List<EFMembership>, List<User>>(allUsers);
            return mappedUsers;
        }
    }
}
