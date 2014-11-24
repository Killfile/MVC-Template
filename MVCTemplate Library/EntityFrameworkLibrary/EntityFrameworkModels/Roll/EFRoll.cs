using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Membership;

namespace Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Permission
{
    public class EFRoll : IEquatable<EFRoll>
    {
        [Key]
        public Guid RollID { get; set; }
        public String Label { get; set; }
        public virtual ICollection<EFPermission> Permissions { get; set; }
        public ICollection<EFMembership> Members { get; set; }

        public bool Equals(EFRoll other)
        {
            return RollID.Equals(other.RollID);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EFRoll) obj);
        }

        public override int GetHashCode()
        {
            return RollID.GetHashCode();
        }
    }
}
