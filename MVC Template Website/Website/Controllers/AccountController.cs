using ContosoUniversity.ViewModels.Account;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace ContosoUniversity.Controllers
{
    public class AccountController : Controller
    {
        private MembershipProvider provider;
        private bool isApproved = true;
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // Get: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationViewModel model) {
            try{
                if(ModelState.IsValid) {
                    
                    MembershipCreateStatus status;
                    MembershipUser user = Membership.CreateUser(model.Username, model.Password, model.Email, model.PasswordQuestion, model.PasswordAnswer, isApproved, null, out status);
                    if (status == MembershipCreateStatus.Success)
                    {
                        return RedirectToAction("RegisterSuccess");
                    }
                    ModelState.AddModelError("", string.Format("Error registering user {0}",status));
                }
            }
                catch (Exception whatBroke) {
                    ModelState.AddModelError("", string.Format("Error registering user: {0}. Please review your submission and try again.  If the problem persists, contact a system administrator.", whatBroke.Message));
                }
            
            return View();
        }

        [Authorize]
        public ActionResult MyAccount()
        {
            MembershipUser user = Membership.GetUser();

            return View(new RegistrationViewModel(user));
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model) {
            bool isValid = Membership.ValidateUser(model.Username, model.Password);
            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(model.Username, true);
                return RedirectToAction("MyAccount");
            }
            ModelState.AddModelError("", "Login Failed.");
            return View();
        }

        
    }
}