using System.Web;
using ContosoUniversity.ViewModels.Account;
using System;
using System.Web.Mvc;
using System.Web.Security;
using Template.Authentication;
using Template.Authentication.Model;

namespace ContosoUniversity.Controllers
{
    public class AccountController : Controller
    {
        private TemplateMembershipProvider _provider;
        private const bool IsApproved = true;

        public AccountController(TemplateMembershipProvider provider)
        {
            this._provider = provider;
        }

        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("MyAccount");
            
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
                if(ModelState.IsValid)
                {
                    _provider.CreateUser(model.Username, model.Password);
                   return RedirectToAction("MyAccount"); 
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
            var ticket = GetFormsAuthTicket();

            User currentUser = _provider.GetUser(ticket.Name);



            return View(new RegistrationViewModel(currentUser));
        }

        private FormsAuthenticationTicket GetFormsAuthTicket()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            return ticket;
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