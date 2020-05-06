using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.Abstract
{
    public class PatientPageAbstract
    {
        private IRepository<Patient> repositoryPatient;
        private IRepository<Doctor> repositoryDoctor;
    }
}