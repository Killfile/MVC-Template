using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using Template.Authentication.Model;

namespace ContosoUniversity.ViewModels.Account
{
    public class RegistrationViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordQuestion { get; set; }

        [Required]
        public string PasswordAnswer { get; set; }

        public RegistrationViewModel()
        {
        }

        public RegistrationViewModel(User user) {
            Username = user.UserName;
            Email = "not in current model";
            Password = "******";

        }
    }
}
