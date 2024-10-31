using Hospital.BLL.Patient.Dto;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Contexts;
using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Repository
{
    public class GenericRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly HospitalDbContext _context;
        public GenericRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public GenericRepository() { }
        public List<TEntity> GetAll()
        => _context.Set<TEntity>().ToList();
       
    }
}
