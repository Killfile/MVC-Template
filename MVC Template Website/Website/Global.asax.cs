using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ContosoUniversity.DAL;
using System.Data.Entity.Infrastructure.Interception;

namespace ContosoUniversity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbInterception.Add(new SchoolInterceptorTransientErrors());
            DbInterception.Add(new SchoolInterceptorLogging());

           InstallMembershipProvider();

        }

        private static void InstallMembershipProvider()
        {

            SchoolContext db = new SchoolContext();

            var installer = new OmidID.Web.Security.Installer();

            installer.MembershipProvider = System.Web.Security.Membership.Provider;
            installer.RoleProvider = System.Web.Security.Roles.Provider;

            //Install database:
            var installed = installer.CreateIfNotExist();
        }
    }
}
