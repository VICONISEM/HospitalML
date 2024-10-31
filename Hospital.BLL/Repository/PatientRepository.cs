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
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalDbContext _context;
        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public List<string> GetAllName()
        {
            return _context.patients.Select(P => P.Name).ToList();
        }

        public List<PatientDto> GetBIByName(string name)
        {
            var Patient = _context.patients.FirstOrDefault(P => P.Name == name);
            var AllBI = _context.BiologicalIndicators.Where(B=>B.PatientId==Patient.Id).ToList();

            List <PatientDto> result= AllBI.Select(B => new PatientDto() { Date = B.Date, Time = B.Time, SugarPercentage = B.SugarPercentage }).ToList();
            return result ;
        }
    }
}
