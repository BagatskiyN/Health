using Health.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Health.WebUI.Infrastructure
{
    public class CustomUserValidator : UserValidator<ApplicationUser,int>
    {
        public CustomUserValidator(AppUserManager manager)
            : base(manager)
        { }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            if (!user.Email.ToLower().Contains("@")&& !user.Email.ToLower().EndsWith(".com"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Email должен иеть формат someone@example.com");
                result = new IdentityResult(errors);
            }
            if ( !user.Email.ToLower().EndsWith(".com"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Email должен заканчиваться на .com");
                result = new IdentityResult(errors);
            }

                return result;
        }
    }
}