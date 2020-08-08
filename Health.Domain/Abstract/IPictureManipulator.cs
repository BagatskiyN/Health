using Health.WebUI.Infrastructure;
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
        FileContentResult GetPatientPhoto(int id);
        FileContentResult GetDoctorPhoto(int id);
        FileContentResult GetSpecializationIcon(int id);

    }
}
