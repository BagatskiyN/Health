using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Entities
{
   public class UpcomingAppointment
    {
        public int UpcomingAppointmentId { get; set; }

        public DateTime UpcomingAppointmentDateTime { get; set; }

        public bool UpcomingAppointmentTookPlace { get; set; }
        public int PatientId { get; set; }
        public string Comment { get; set; }

        public int DoctorId { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
