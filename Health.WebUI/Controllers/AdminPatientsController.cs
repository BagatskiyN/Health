using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Infrastructure;
using Health.WebUI.Models.AdminPatients;
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
    public class AdminPatientsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
    
        public AdminPatientsController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        // GET: AdminPatients
        public ActionResult Index()
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
        public async Task<ActionResult> Delete(int id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                Patient patient = unitOfWork.Patients.FindById(user.Id);
                if (patient != null)
                {
                    unitOfWork.Patients.Remove(patient);
                }

                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    unitOfWork.Patients.Create(patient);
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Пользователь не найден" });
            }
        }

        public async Task<ActionResult> EditPatient(int id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            Patient patient = unitOfWork.Patients.FindById(id);
            if (user != null && patient != null)
            {
                EditPatientViewModel editPatientViewModel = new EditPatientViewModel()
                {
                    Id = user.Id,
                    Name = patient.Name,
                    Patronymic = patient.Patronymic,
                    Surname = patient.Surname,
                    PatientHeight = patient.PatientHeight,
                    PatientWeight = patient.PatientWeight,
                    BloodTypeId = patient.BloodTypeId,
                    GenderId = patient.GenderId,
                    Email = user.Email,
                    PatientBirthdate = patient.PatientBirthdate,
                    ImageMimeType=patient.ImageMimeType,
                    ImageData=patient.ImageData
                    
                };
                ViewBag.BloodTypes = unitOfWork.BloodTypes.Get().ToList();
                ViewBag.Genders = unitOfWork.Genders.Get().ToList();
                return View(editPatientViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<ActionResult> EditPatient(EditPatientViewModel editPatientViewModel, HttpPostedFileBase imageInp)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(editPatientViewModel.Id);
            if (user != null)
            {
                user.Email = editPatientViewModel.Email;
                IdentityResult validEmail
                    = await UserManager.UserValidator.ValidateAsync(user);

                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (editPatientViewModel.Password != null)
                {
                    validPass
                        = await UserManager.PasswordValidator.ValidateAsync(editPatientViewModel.Password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                            UserManager.PasswordHasher.HashPassword(editPatientViewModel.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                IdentityResult result = null;
                if ((validEmail.Succeeded && validPass == null) ||
                        (validEmail.Succeeded && editPatientViewModel.Password != string.Empty && validPass.Succeeded))
                {
                    user.UserName = editPatientViewModel.Email;
               result = await UserManager.UpdateAsync(user);
                  
                }

                Patient patient = unitOfWork.Patients.FindById(editPatientViewModel.Id);
                patient.Name = editPatientViewModel.Name;
                patient.Surname = editPatientViewModel.Surname;
                patient.Patronymic = editPatientViewModel.Patronymic;
                patient.PatientHeight = editPatientViewModel.PatientHeight;
                patient.PatientWeight = editPatientViewModel.PatientHeight;
                patient.BloodTypeId = editPatientViewModel.BloodTypeId;
                patient.GenderId = editPatientViewModel.GenderId;
                if (imageInp != null)
                {
                    patient.ImageMimeType = imageInp.ContentType;
                    patient.ImageData = new byte[imageInp.ContentLength];
                    imageInp.InputStream.Read(patient.ImageData, 0, imageInp.ContentLength);
                }
                unitOfWork.Patients.Update(patient);
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
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        //public FileContentResult GetImage(int patientId)
        //{
        //    return pictureManipulator.GetPatientPhoto(patientId);
        //}
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}