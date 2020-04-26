using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Health.Domain.Entities;

namespace Health.Domain.Concrete
{
    class EFDbContext:DbContext
    {
        public EFDbContext():base("EFDbContext")
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<PreviousAppointment> PreviousAppointments { get; set; }
        public DbSet<UpcomingAppointment> UpcomingAppointments { get; set; }

    }
}
