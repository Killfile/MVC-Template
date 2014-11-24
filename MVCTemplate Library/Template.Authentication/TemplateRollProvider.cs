using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Authentication.Model;
using Template.Authentication.Persistance;
using Template.Extensions;
using Template.PersistanceContract.Membership;

namespace Template.Authentication
{
    public class TemplateRollProvider
    {
        private readonly IPermissionPersistance _permissionPersistance;
        private readonly IRollPersistance _rollPersistance;
        private readonly IMembershipPersistance _membershipPersistance;

        public TemplateRollProvider(IRollPersistance rollPersistance, IPermissionPersistance permissionPersistance, IMembershipPersistance membershipPersistance)
        {
            _rollPersistance = rollPersistance;
            _permissionPersistance = permissionPersistance;
            _membershipPersistance = membershipPersistance;
        }

        private List<Roll> GetRollsForUser(Guid userID)
        {
            User user = _membershipPersistance.GetByID(userID);
            return user.Rolls;
        }

        private List<Permission> GetPermissionsForUser(Guid userID)
        {
            User user = _membershipPersistance.GetByID(userID);
            return user.Rolls.SelectMany(r => r.Permissions).DistinctBy(p => p.PermissionID).ToList();
        }

        private void SetRollsForUser(Guid userID, List<Guid> rollIDs)
        {
            User user = _membershipPersistance.GetByID(userID);
            List<Roll> rolls = _rollPersistance.GetAll(rollIDs);
            user.Rolls = rolls;
            _membershipPersistance.Update(user);
        }

        private bool UserHasPermission(Guid userID, Guid permissionID)
        {
            User user = _membershipPersistance.GetByID(userID);
            bool hasPermission = user.Rolls.Exists(r => r.Permissions.Exists(p => p.PermissionID == permissionID));
            return hasPermission;
        }

        private void SetPermissionsForRoll(Guid rollID, List<Guid> permissionIDs)
        {
            var Permissions = _permissionPersistance.GetAll(permissionIDs);
            var Roll = _rollPersistance.Get(rollID);
            Roll.Permissions = Permissions;
            _rollPersistance.Update(Roll);

        }
    }
}
