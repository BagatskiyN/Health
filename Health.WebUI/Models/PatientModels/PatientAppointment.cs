using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.PatientModels
{
    public class PatientAppointment
    {
        public Doctor Doctor { get; set; }
        public Appointment Appointment { get; set; }

    }
}