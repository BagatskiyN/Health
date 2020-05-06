using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Concrete
{
    public class EFGenderRepository : IRepository<Gender>
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Gender> GetItemList => throw new NotImplementedException();

        public void Create(Gender item)
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

        public Gender GetItem(int id)
        {
            return context.Genders.FirstOrDefault(p => p.GenderId == id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Gender item)
        {
            throw new NotImplementedException();
        }
    }
}
