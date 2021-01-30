using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Infrastructure;
using Health.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Health.WebUI.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
       readonly IUnitOfWork unitOfWork;
        public AdminController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        } 
        public ActionResult Index()
        {
          
            return View();
        }
        // GET: Admin
      
       //Методы работы с пациентами
        public ActionResult ShowPatientsList()
        {
            var users = new List<ApplicationUser>();
            var roles = new List<string>();
            foreach (var user in UserManager.Users.ToList())
            {
                foreach (var role in UserManager.GetRoles(user.Id))
                {
                    if (role.ToString() == "Patients")
                    {
                        users.Add(user);
                    }
                }
            }
            return View(users);
        }
        
        //Методы работы с докторами
        public ActionResult ShowDoctorsList()
        {
            var users = new List<ApplicationUser>();
            var roles = new List<string>();
            foreach (var user in UserManager.Users.ToList())
            {
                foreach (var role in UserManager.GetRoles(user.Id))
                {
                    if (role.ToString() == "Doctors")
                    {
                        users.Add(user);
                    }
                }
            }
            return View("Index","AdminDoctors",users);
        }

        public ActionResult CreateDoctor()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateDoctor(CreateDoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                IdentityResult result =
                    await UserManager.CreateAsync(user, model.Password);
                ApplicationUser applicationUser= UserManager.FindByEmail(user.Email);

             

                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                if (result.Succeeded)
                {   
                    result = await UserManager.AddToRoleAsync(applicationUser.Id, "Administrators");
              
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }


 
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    
        
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
               Doctor doctor= unitOfWork.Doctors.FindById(user.Id);
                if(doctor!=null)
                {
                    unitOfWork.Doctors.Remove(doctor);
                }

                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    unitOfWork.Doctors.Create(doctor);
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Пользователь не найден" });
            }
        }
        public async Task<ActionResult> Edit(int id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Edit(int id, string email, string password)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail
                    = await UserManager.UserValidator.ValidateAsync(user);

                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass
                        = await UserManager.PasswordValidator.ValidateAsync(password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                            UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) ||
                        (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return View(user);
        }
    }
}