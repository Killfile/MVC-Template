using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Permission
{
    public class EFPermission
    {
        [Key]
        public Guid PermissionID { get; set; }
        public String Label { get; set; }
        public virtual ICollection<EFRoll> Rolls { get; set; }
    }
}
