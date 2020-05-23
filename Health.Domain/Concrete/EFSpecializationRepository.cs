﻿using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Concrete
{
  public  class EFSpecializationRepository : IRepository<Specialization>
    {
        EFDbContext context = new EFDbContext();
        public void Create(Specialization item)
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

        public Specialization GetItem(int id)
        {
            return context.Specializations.FirstOrDefault(p => p.SpecializationId == id);
        }

        public IEnumerable<Specialization> GetItemList
        { get; }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Specialization item)
        {
            throw new NotImplementedException();
        }
    }
}