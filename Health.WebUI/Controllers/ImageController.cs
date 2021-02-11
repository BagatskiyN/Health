using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.WebUI.Controllers
{
    public class ImageController : Controller,IPictureManipulator
    {
        // GET: Image
        IUnitOfWork unitOfWork;
        public ImageController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public ActionResult GetDoctorPhoto(int id)
        {

            Doctor doctor = unitOfWork.Doctors.FindById(id);


            if (doctor != null && doctor.ImageData != null && doctor.ImageMimeType != null)
            {
                return File(doctor.ImageData, doctor.ImageMimeType);
            }
            else
            {
                var dir = Server.MapPath("~/Content/icons/svgIcons");
                var path = Path.Combine(dir, "UserHaveNoPhoto.jpg"); 
                return File(path, "image/jpeg");
            }
            }
        public ActionResult GetPatientPhoto(int id)
        {

            Patient patient = unitOfWork.Patients.FindById(id);


            if (patient != null && patient.ImageData != null && patient.ImageMimeType != null)
            {
                return File(patient.ImageData, patient.ImageMimeType);
            }
            else
            {
                var dir = Server.MapPath("~/Content/icons/svgIcons");
                var path = Path.Combine(dir, "UserHaveNoPhoto.jpg");
                return File(path, "image/jpeg");
            }
        }
        public ActionResult GetSpecializationIcon(int id)
        {
            Specialization specialization = unitOfWork.Specializations.FindById(id);


            if (specialization != null && specialization.SpecializationImageData != null && specialization.SpecializationImageMimeType != null)
            {
                return File(specialization.SpecializationImageData, specialization.SpecializationImageMimeType);
            }
            else
            {
                var dir = Server.MapPath("~/Content/icons/svgIcons");
                var path = Path.Combine(dir, "SpecializationNoIcon.png");
                return File(path, "image/png");
            }
        }

    }


}