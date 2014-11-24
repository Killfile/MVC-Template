using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Authentication.Model;

namespace Template.Authentication.Persistance
{
    public interface IRollPersistance
    {
        void Insert(Roll roll);
        void Update(Roll roll);
        void Delete(Guid RoleId);
        List<Roll> GetAll();
        Roll Get(Guid RoleId);


        List<Roll> GetAll(List<Guid> rollIDs);
    }
}
