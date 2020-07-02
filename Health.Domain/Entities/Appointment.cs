using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Entities
{
   public class Appointment
    {
     
        public int AppointmentId { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public string  AppointmentPlace { get; set; }

        public string AppointmentComment { get; set; }


        public Diagnosis Diagnosis { get; set; }

        public int? PatientId { get; set; }

        public int? DoctorId { get; set; }


        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }

    }
}
