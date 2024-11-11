using Hospital.BLL.PatientServices.Dto;
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
    public class PatientRepository : GenericRepository<Patient>,IPatientRepository
    {
        private readonly HospitalDbContext _context;
        public PatientRepository(HospitalDbContext context):base(context) 
        {
            _context = context;
        }
        public List<string> GetAllName()
        {
            return _context.patients.Select(P => P.Name).ToList();
        }

        public List<BiologicalIndicators> GetBIByName(string name)
        {
            var Patient = _context.patients.Where(P => P.Name.Trim().ToLower() == name.Trim().ToLower()).SelectMany(p=>p.biologicalIndicators).ToList();
            
            return Patient;
        }

      
    }
}
