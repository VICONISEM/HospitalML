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
    public  class HospitalRepository:GenericRepository<Hospitals>,IHospitalRepository
    {
        public HospitalDbContext _context { get; set; }

        public HospitalRepository(HospitalDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
