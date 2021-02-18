using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Health.Domain.Concrete
{
    public class PictureManipulator : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public PictureManipulator(IUnitOfWork _unitOfWork)
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
                return null;
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
                return null;
            }
        }
        public ActionResult GetSpecializationIcon(int id)
        {
            Patient patient = unitOfWork.Patients.FindById(id);


            if (patient != null && patient.ImageData != null && patient.ImageMimeType != null)
            {
                return File(patient.ImageData, patient.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

    }

}
