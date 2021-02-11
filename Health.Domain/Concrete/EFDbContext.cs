using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Health.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

using Microsoft.AspNet.Identity;
using Health.WebUI.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
//using Health.WebUI.Infrastructure;

namespace Health.Domain.Concrete
{
    public class EFDbContext : IdentityDbContext<ApplicationUser, CustomRole,
      int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public EFDbContext() : base("EFDbContext")
        {
            Database.SetInitializer<EFDbContext>(new DropCreateDatabaseIfModelChanges<EFDbContext>());
            Database.SetInitializer<EFDbContext>(new IdentityDbInit());
        }
        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<BloodType> BloodTypes { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                  .HasOptional(c => c.Patient)
                  .WithRequired(d => d.ApplicationUser);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(c => c.Doctor)
                .WithRequired(d => d.ApplicationUser);
            base.OnModelCreating(modelBuilder);
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            PerformInitialSetupAsync(context);
            base.Seed(context);
        }
        public async Task PerformInitialSetupAsync(EFDbContext context)
        {

            AppUserManager userMgr = new AppUserManager(new UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<CustomRole, int, CustomUserRole>(context));

            string roleAdmin= "Administrators";
            string rolePatient = "Patients";
            string userName = "Admin";
            string password = "Password1234";
            string email = "admin@gmail.com";

            if (!roleMgr.RoleExists(roleAdmin))
            {
                IdentityResult identityResult = roleMgr.Create(new CustomRole() { Name = roleAdmin });
            }
            if (!roleMgr.RoleExists(rolePatient))
            {
                IdentityResult identityResult = roleMgr.Create(new CustomRole() { Name = rolePatient });
            }

            ApplicationUser user = new ApplicationUser { UserName = email, Email = email };
            IdentityResult result =
                await userMgr.CreateAsync(user, password);
            ApplicationUser applicationUser = userMgr.FindByEmail(user.Email);



            if (result.Succeeded)
            {
                result = await userMgr.AddToRoleAsync(applicationUser.Id, "Administrators");
            }
        }
        public class AppUserManager : UserManager<ApplicationUser, int>
        {
            public AppUserManager(IUserStore<ApplicationUser, int> store)
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
                manager.UserValidator = new UserValidator<ApplicationUser, int>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                return manager;
            }
        }
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
}
