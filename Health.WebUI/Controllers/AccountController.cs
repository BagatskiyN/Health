using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Infrastructure;
using Health.WebUI.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Health.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public AccountController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "В доступе отказано" });
            }


            ViewBag.returnUrl = returnUrl;
            return View();
        }

        private string GetUrlByRole(ApplicationUser user)
        {
            var role = UserManager.GetRoles(user.Id);


            if (role[0] == "Administrators")
            {
                return "Admin";
            }
            else
            {
                if (role[0] == "Patients")
                {
                    return "Patient";
                }
                else
                {
                    if (role[0] == "Doctors")
                    {
                        return "Doctor";
                    }
                    else
                    {
                        throw new Exception("Такой роли нет!");
                    }
                }
            }
        }

        [AllowAnonymous]
        public ActionResult CreatePatient()
        {
            ViewBag.BloodTypes = new SelectList(unitOfWork.BloodTypes.Get().ToList(), "BloodTypeId", "BloodTypeTitle");
            ViewBag.Genders = new SelectList(unitOfWork.Genders.Get().ToList(), "GenderId", "GenderTitle");

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreatePatient(CreatePatientViewModel model)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                IdentityResult result =
                    await UserManager.CreateAsync(user, model.Password);
                ApplicationUser applicationUser = UserManager.FindByEmail(user.Email);



                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                if (result.Succeeded)
                {
                    try
                    {
                        result = await UserManager.AddToRoleAsync(applicationUser.Id, "Patients");
                        BloodType bloodType;
                        if (model.BloodTypeId != null)
                        {
                           bloodType = unitOfWork.BloodTypes.FindById((int)model.BloodTypeId);
                        }
                        else
                        {
                          bloodType = unitOfWork.BloodTypes.Get(x=>x.BloodTypeTitle=="-").FirstOrDefault();

                        }
                        Gender gender;
                        if (model.GenderId != null)
                        {
                           gender = unitOfWork.Genders.FindById((int)model.GenderId);
                        }
                        else
                        {
                           gender = unitOfWork.Genders.Get(x => x.GenderTitle == "-").FirstOrDefault();

                        }

                      
                        Patient patient = new Patient()
                        {
                            Id = applicationUser.Id,
                            Name = model.Name,
                            Surname = model.Surname,
                            Patronymic = model.Patronymic,
                            PatientBirthdate = model.BirthDate,
                            BloodType =bloodType,
                            BloodTypeId = bloodType.BloodTypeId,
                            GenderId = gender.GenderId,
                            Gender = gender
                        };

                        unitOfWork.Patients.Create(patient);
                    }
                    catch (NullReferenceException e)
                    {
                        UserManager.Delete(applicationUser);
                    }

                    return RedirectToAction("Index", "Patient");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel details, string returnUrl)
        {
            ApplicationUser user = await UserManager.FindAsync(details.Email, details.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Некорректное имя или пароль.");
            }
            else
            {
                ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);

                returnUrl = GetUrlByRole(user);
                return RedirectToAction("Index", returnUrl);
            }

            return View(details);
        }
        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}