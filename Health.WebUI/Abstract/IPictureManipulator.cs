using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Health.WebUI.Abstract
{
    public interface IPictureManipulator
    {
         ActionResult GetDoctorPhoto(int id);
         ActionResult GetPatientPhoto(int id);
         ActionResult GetSpecializationIcon(int id);

    }
}
