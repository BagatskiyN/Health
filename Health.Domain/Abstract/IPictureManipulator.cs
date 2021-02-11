
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Health.Domain.Abstract
{
   public interface IPictureManipulator
    {
        ActionResult GetPatientPhoto(int id);
        ActionResult GetDoctorPhoto(int id);
        ActionResult GetSpecializationIcon(int id);

    }
}
