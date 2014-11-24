using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Template.Authentication;
using Template.Authentication.Model;
using Template.MappingContract;
using Template.Website.ViewModels.Account;

namespace Template.Website.Controllers
{
    public class AccountController : Controller
    {
        private TemplateMembershipProvider _provider;
        private readonly MappingBaseline _mapper;
        private const bool IsApproved = true;

        public AccountController(TemplateMembershipProvider provider, MappingBaseline mapper)
        {
            _provider = provider;
            _mapper = mapper;
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

            var viewModel = _mapper.Map<User, RegistrationViewModel>(currentUser);

            return View(viewModel);
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
            Debug.Assert(_provider != null, "_provider != null");
            bool isValid = _provider.ValidateUser(model.Username, model.Password);
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