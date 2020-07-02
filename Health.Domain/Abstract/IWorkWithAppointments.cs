using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.WebUI.Models.Abstract
{
   public interface IWorkWithAppointments
    {
         List<Appointment> SkipUsedAppointments(List<Appointment> appointments, int page, int pageSize);
        List<Appointment> SkipUsedNextAppointments(List<Appointment> appointments, int page, int nextPageSize);
     }
}
