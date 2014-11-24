using System;
using System.ComponentModel.DataAnnotations;

namespace Template.Website.ViewModels.Membership.Permissions
{
    public class CreatePermissionViewModel
    {
        [Required]
        public String Label { get; set; }

    }
}