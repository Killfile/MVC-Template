using System;
using System.ComponentModel.DataAnnotations;

namespace Template.Website.ViewModels.Membership.Permissions
{
    public class EditPermissionViewModel : CreatePermissionViewModel
    {
    

        [Required]
        public Guid PermissionID { get; set; }
    }
}