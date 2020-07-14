using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Health.WebUI.Infrastructure
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, int id)
        {
            AppUserManager mgr = HttpContext.Current
                .GetOwinContext().GetUserManager<AppUserManager>();

            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }
    }
}