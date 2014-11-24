using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Permission;


namespace Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Membership
{
    public class EFMembership
    {
       
        [Key]
        public Guid UserId { get; set; }
        public String UserName { get; set; }
        public String HashedPassword { get; set; }
        public String Salt { get; set; }

        public virtual ICollection<EFRoll> Rolls { get; set; } 
    }
}
