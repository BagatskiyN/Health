using Health.Domain.Concrete;
using Health.Domain.Entities;
using Health.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Infrastructure
{
    public class AppRoleManager : RoleManager<CustomRole, int>, IDisposable
    {
        public AppRoleManager(RoleStore<CustomRole, int, CustomUserRole> store)
            : base(store)
        { }

        public static AppRoleManager Create(
            IdentityFactoryOptions<AppRoleManager> options,
            IOwinContext context)
        {
            return new AppRoleManager(new
                RoleStore<CustomRole, int, CustomUserRole>(context.Get<EFDbContext>()));
        }
    }
}