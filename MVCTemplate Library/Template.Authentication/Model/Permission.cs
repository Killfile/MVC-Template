using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Authentication.Model
{
    public class Permission
    {
        public Guid PermissionID { get; set; }
        public String Label { get; set; }
        public List<Roll> Rolls { get; set; }
    }
}
