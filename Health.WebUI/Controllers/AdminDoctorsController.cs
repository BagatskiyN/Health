using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Infrastructure;
using Health.WebUI.Models.AdminDoctors;
using Microsoft.AspNet.Identity;
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
    public class AdminDoctorsController : Controller
    {
        IUnitOfWork unitOfWork;
       public AdminDoctorsController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        // GET: AdminDoctors
        public ActionResult Index()
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
            return View(users);  
        }
        public async Task<ActionResult> EditDoctor(int id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            Doctor doctor = unitOfWork.Doctors.FindById(id);
            if (user != null && doctor != null)
            {
                EditDoctorViewModel editPatientViewModel = new EditDoctorViewModel()
                {
                    Id = user.Id,
                    Name = doctor.Name,
                    Patronymic = doctor.Patronymic,
                    Surname = doctor.Surname,
                    SpecializationId =(int)doctor.SpecializationId,
                    GenderId = doctor.GenderId,
                    Email = user.Email,  
                    ImageMimeType = doctor.ImageMimeType,
                    ImageData = doctor.ImageData

                };
                ViewBag.Specializations = unitOfWork.Specializations.Get().ToList().OrderBy(s=>s.SpecializationTitle);
                ViewBag.Genders = unitOfWork.Genders.Get().ToList();
                return View(editPatientViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<ActionResult> EditDoctor(EditDoctorViewModel editDoctorViewModel, HttpPostedFileBase imageInp)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(editDoctorViewModel.Id);
            if (user != null)
            {
                user.Email = editDoctorViewModel.Email;
                IdentityResult validEmail
                    = await UserManager.UserValidator.ValidateAsync(user);

                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (editDoctorViewModel.Password != null)
                {
                    validPass
                        = await UserManager.PasswordValidator.ValidateAsync(editDoctorViewModel.Password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                            UserManager.PasswordHasher.HashPassword(editDoctorViewModel.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                IdentityResult result = null;
                if ((validEmail.Succeeded && validPass == null) ||
                        (validEmail.Succeeded && editDoctorViewModel.Password != string.Empty && validPass.Succeeded))
                {
                    user.UserName = editDoctorViewModel.Email;
                    result = await UserManager.UpdateAsync(user);

                }

                Doctor doctor = unitOfWork.Doctors.FindById(editDoctorViewModel.Id);
                doctor.Name = editDoctorViewModel.Name;
                doctor.Surname = editDoctorViewModel.Surname;
                doctor.Patronymic = editDoctorViewModel.Patronymic;
                doctor.DoctorPhone = editDoctorViewModel.DoctorPhone;
                doctor.SpecializationId = editDoctorViewModel.SpecializationId;
                doctor.GenderId = editDoctorViewModel.GenderId;
                if (imageInp != null)
                {
                    doctor.ImageMimeType = imageInp.ContentType;
                    doctor.ImageData = new byte[imageInp.ContentLength];
                    imageInp.InputStream.Read(doctor.ImageData, 0, imageInp.ContentLength);
                }
                unitOfWork.Doctors.Update(doctor);
                if (result != null)
                {
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
        public async Task<ActionResult> Delete(int id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                Doctor doctor = unitOfWork.Doctors.FindById(user.Id);
                if (doctor != null)
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

            private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        public ActionResult CreateDoctor()
        {
            ViewBag.Specializations = unitOfWork.Specializations.Get().ToList().OrderBy(s => s.SpecializationTitle);
            ViewBag.Genders = unitOfWork.Genders.Get().ToList();
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateDoctor(EditDoctorViewModel model, HttpPostedFileBase imageInp)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email,EmailConfirmed=true };
                IdentityResult result =
                    await UserManager.CreateAsync(user, model.Password);
                ApplicationUser applicationUser = UserManager.FindByEmail(user.Email);



                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(applicationUser.Id, "Doctors");

                    Doctor doctor = new Doctor()
                    {
                        Id = applicationUser.Id,
                        Name = model.Name,
                        Surname = model.Surname,
                        Patronymic = model.Patronymic,
                        DoctorPhone=model.DoctorPhone,
                        SpecializationId = model.SpecializationId,
                        GenderId = model.GenderId
                    };
                    if (imageInp != null)
                     {
                        doctor.ImageMimeType = imageInp.ContentType;
                        doctor.ImageData = new byte[imageInp.ContentLength];
                        imageInp.InputStream.Read(doctor.ImageData, 0, imageInp.ContentLength);
                     }

                    unitOfWork.Doctors.Create(doctor);
                    return RedirectToAction("Index", "AdminDoctors");
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
    }
}