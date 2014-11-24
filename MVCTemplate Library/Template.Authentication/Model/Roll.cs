using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Authentication.Model
{
    public class Roll
    {
        public Guid RollID { get; set; }
        public String Label { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
