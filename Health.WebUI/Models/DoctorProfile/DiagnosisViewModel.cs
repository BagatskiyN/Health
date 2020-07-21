using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.DoctorProfile
{
    public class DiagnosisViewModel
    {
        public int DiagnosisId { get; set; }

        public int? DiseaseId { get; set; }
        
        public string DiagnosisComment { get; set; }

        public string DiagnosisRecommendations { get; set; }

        public string DiseaseTitle { get; set; }
       
    }
}