using Oren.Model.Model.Entities;
using Oren.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ForOrensj.Attributes
{
    public class RoleAttribute : AuthorizeAttribute
    {

        private UserService db;
        private string[] _roles;
        public RoleAttribute(params string[] roles)
        {
            db = new UserService();
            _roles = new string[roles.Length];
            Array.Copy(roles, _roles, roles.Length);
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.Name != "")
            {
                AppUser currentUser = db.UserFromName(HttpContext.Current.User.Identity.Name);

                foreach (var item in _roles)
                {
                    if (currentUser.Role.ToString().ToLower() == item.ToLower())
                        return true;
                }
            }
            HttpContext.Current.Response.Redirect("~/Error/NFound");
            return false;

        }
    }
}