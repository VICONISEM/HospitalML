using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Contexts;
using Hospital.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Repository
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly HospitalDbContext _context;
        public PatientRepository(HospitalDbContext context):base(context) 
        {
            _context = context;
        }
        public async Task<List<PatientDtoName>> GetAllName()
        {
            return await _context.patients.Select(p=>new PatientDtoName() { Id=p.Id, Age=p.Age, Name = p.Name, LastBiologicalIndicator = p.biologicalIndicators.OrderByDescending(b => b.Date).ThenByDescending(b => b.Time).Select(l=> new LastBiologicalIndicatorDTO() {AverageTemprature=l.AverageTemprature,BloodPressure=l.BloodPressure,HealthCondition=l.HealthCondition,SugarPercentage=l.SugarPercentage}).FirstOrDefault(),HospitalId=p.HospitalId }).ToListAsync();
        }

        public async Task<List<BiologicalIndicators>> GetBIByName(string name)
        {
            var Patient = await _context.patients.Where(P => P.Name.Trim().ToLower() == name.Trim().ToLower()).SelectMany(p=>p.biologicalIndicators).ToListAsync();
            
            return Patient;
        }
	}
}
