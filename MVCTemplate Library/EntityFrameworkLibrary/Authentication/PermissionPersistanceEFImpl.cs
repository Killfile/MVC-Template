using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniversity.DAL;
using Template.Authentication.Model;
using Template.Authentication.Persistance;
using Template.Extensions;
using Template.MappingContract;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Permission;

namespace Template.Persistance.EntityFrameworkImpl.Authentication
{
    public class PermissionPersistanceEFImpl : IPermissionPersistance 
    {
        private readonly TemplateDBContext _context;

        private readonly MappingBaseline _mapper;

        public PermissionPersistanceEFImpl(MappingBaseline mapper, TemplateDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Insert(Permission Permission)
        {
            EFPermission efPermission = _mapper.Map<Permission, EFPermission>(Permission);
            _context.Permissions.Add(efPermission);
            _context.SaveChanges();
        }

        public void Update(Permission Permission)
        {
            EFPermission mappedPermission = _mapper.Map<Permission, EFPermission>(Permission);
            EFPermission existingPermission = _context.Permissions.FirstOrDefault(p => p.PermissionID == mappedPermission.PermissionID);
            

            UpdateProperties(existingPermission, mappedPermission);
            UpdateAssociations(existingPermission, mappedPermission);
            _context.Entry(existingPermission).State = EntityState.Modified;
            _context.SaveChanges();

        }

        private void UpdateAssociations(EFPermission existingPermission, EFPermission efPermission)
        {
            _context.Entry(existingPermission).Collection(p=>p.Rolls).Load();

            List<EFRoll> removedRolls = existingPermission.Rolls.Except(efPermission.Rolls, new PropertyComparer<EFRoll>("RollID")).ToList();

            List<EFRoll> addedRolls = efPermission.Rolls.Except(existingPermission.Rolls, new PropertyComparer<EFRoll>("RollID")).ToList();

            removedRolls.ForEach(r => existingPermission.Rolls.Remove(r));

            foreach (EFRoll addedRoll in addedRolls)
            {
               // if (_context.Entry(addedRoll).State == EntityState.Detached)
                 //   _context.Set<EFRoll>().Attach(addedRoll);
                    //_context.Rolls.Attach(addedRoll);

                existingPermission.Rolls.Add(_context.Rolls.Find(addedRoll.RollID));
            }
        }

        private static void UpdateProperties(EFPermission existingPermission, EFPermission efPermission)
        {
            existingPermission.Label = efPermission.Label;
        }

        public void Delete(Guid RoleId)
        {
            EFPermission targetPermission = GetEFPermissionByID(RoleId);
            _context.Permissions.Remove(targetPermission);
            _context.SaveChanges();
        }

        public List<Permission> GetAll()
        {
            var returnValue = _mapper.Map<List<EFPermission>, List<Permission>>(_context.Permissions.ToList());
            return returnValue;
        }

        public Permission Get(Guid RoleId)
        {
            EFPermission targetPermission = GetEFPermissionByID(RoleId);
            if (targetPermission == null)
                return null;
            return _mapper.Map<EFPermission, Permission>(targetPermission);
        }

        public List<Permission> GetAll(List<Guid> permissionIDs)
        {
            List<EFPermission> candidateValues = _context.Permissions.Where(p => permissionIDs.Contains(p.PermissionID)).ToList();
            return _mapper.Map<List<EFPermission>, List<Permission>>(candidateValues);
        } 

        private EFPermission GetEFPermissionByID(Guid RoleId)
        {
            EFPermission targetPermission = _context.Permissions.Find(RoleId);
            return targetPermission;
        }


    }
}
