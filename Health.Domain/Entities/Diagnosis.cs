using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Domain.Entities
{
   public class Diagnosis
    {
        [Key]
        [ForeignKey("PreviousAppointment")]
        public int DiagnosisId { get; set; }
        public string DiagnosisTitle { get; set; }
        public string DiagnosisComment { get; set; }
        public string DiagnosisRecommendations { get; set; }
        public int PatientId { get; set; }

        public int DoctorId { get; set; }
        public PreviousAppointment PreviousAppointment { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }

    }
}
