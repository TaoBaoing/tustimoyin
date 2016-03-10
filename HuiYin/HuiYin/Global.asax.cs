using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using HuiYin.Models;

namespace HuiYin
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<AppDbContext>(null);
            
        }

        public WebApiApplication()
        {

            AuthenticateRequest += WebApiApplication_AuthenticateRequest;
        }

        void WebApiApplication_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (null == authCookie)
            {
                return;
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);//解密
            // string[] roles = authTicket.UserData.Split(new char[] { ';' });//根据存入时的格式分解，;或|....
            if (null == authTicket)
            {
                return;
            }
            //FormsIdentity identity = new FormsIdentity(authTicket);
            // string userId = authTicket.UserData;
            var user = new LhUser(authTicket.Name);
            
           HttpContext.Current.User = user;
        }
    }
}
