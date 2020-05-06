using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Health.Domain.Concrete
{
    public class EFDoctorRepository : IRepository<Doctor>
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Doctor> GetItemList => throw new NotImplementedException();

        public void Create(Doctor item)
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

        public Doctor GetItem(int id)
        {
            return context.Doctors.FirstOrDefault(p => p.DoctorId == id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Doctor item)
        {
            throw new NotImplementedException();
        }
    }
}
