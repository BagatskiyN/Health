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
        [ForeignKey("Appointment")]
        public int DiagnosisId { get; set; }
  
        public string DiagnosisComment { get; set; }
        public string DiagnosisRecommendations { get; set; }
      
        public int? DiseaseId { get; set; }
        public virtual Disease Disease { get; set; }
   
        public Appointment Appointment { get; set; }


    }
}
