using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health.Domain.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Health.Domain.Concrete;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Health.WebUI.Infrastructure
{
    public class AppUserManager : UserManager<ApplicationUser,int>
    {
        public AppUserManager(IUserStore<ApplicationUser,int> store)
            : base(store)
        { }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
            IOwinContext context)
        {
            EFDbContext db = context.Get<EFDbContext>();
            AppUserManager manager = new AppUserManager(new CustomUserStore(db));
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = false
            };
            manager.UserValidator = new UserValidator<ApplicationUser,int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            return manager;
        }
    }
}