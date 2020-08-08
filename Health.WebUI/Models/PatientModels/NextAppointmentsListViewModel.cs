using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.PatientModels
{
    public class NextAppointmentsListViewModel
    {
        public IEnumerable<PatientAppointment> PatientAppointments { get; set; }
        

    }
}