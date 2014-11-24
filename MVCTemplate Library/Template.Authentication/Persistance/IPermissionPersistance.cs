using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Authentication.Model;

namespace Template.Authentication.Persistance
{
    public interface IPermissionPersistance
    {
        void Insert(Permission permission);
        void Update(Permission permission);
        void Delete(Guid permissionId);
        List<Permission> GetAll();
        Permission Get(Guid permissionId);
        List<Permission> GetAll(List<Guid> permissionIDs);
    }
}
