using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace Template.Website.ViewModels.Membership.Rolls
{
    public class EditRollViewModel : CreateRollViewModel
    {
        [Required]
        public Guid RollID { get; set; }
    }
}