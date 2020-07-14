using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Health.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Health.WebUI.Infrastructure;
using Microsoft.AspNet.Identity;
using Health.WebUI.Models;

namespace Health.Domain.Concrete
{
    public class EFDbContext : IdentityDbContext<ApplicationUser, CustomRole,
      int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public EFDbContext() : base("EFDbContext")
        {
            //Database.SetInitializer<EFDbContext>(new DropCreateDatabaseIfModelChanges<EFDbContext>());
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
            // base.OnModelCreating(modelBuilder);

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
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(EFDbContext context)
        {
            AppUserManager userMgr = new AppUserManager(new UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<CustomRole, int, CustomUserRole>(context));

            string roleName = "Administrators";
            string userName = "Admin";
            string password = "Password";
            string email = "admin@gmail.com";

            if (!roleMgr.RoleExists(roleName))
            {
                IdentityResult identityResult = roleMgr.Create(new CustomRole() { Name = roleName });
            }

            ApplicationUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new ApplicationUser { UserName = userName, Email = email },
                    password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }
        }

    }
}
