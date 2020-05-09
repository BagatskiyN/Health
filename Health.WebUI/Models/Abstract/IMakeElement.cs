using Health.Domain.Entities;
using Health.WebUI.Models.PatientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.WebUI.Models.Abstract
{
   public interface IMakeElement<T>
        where T:class
    {
        T MakeElement(int patientId);
    }
}
