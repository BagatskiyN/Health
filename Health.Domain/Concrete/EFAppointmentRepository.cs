using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Concrete
{
  public  class EFAppointmentRepository : IRepository<Appointment>
    {
        EFDbContext context = new EFDbContext();
        
        public void Create(Appointment item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Appointment> GetItemList
        {
            get { return context.Appointments; }
        }
        public Appointment GetItem(int id)
        {
            return context.Appointments.FirstOrDefault(p => p.AppointmentId == id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Appointment item)
        {
            throw new NotImplementedException();
        }
    }
}
