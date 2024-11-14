using AutoMapper;
using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.Repository;
using Hospital.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.PatientServices.Service
{
    public class PatientService : IPatientService
    {
        private readonly HospitalDbContext _context;
        private readonly IMapper _mapper;

		public PatientService(HospitalDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public PatientRepository _patientRepository {

         get 
            {
                return new PatientRepository(_context);
            
            
            } 
        }
        public BiologicalIndicatorsRepository _BiologicalIndicatorsRepositoryRepository
        {

            get
            {
                return new BiologicalIndicatorsRepository(_context);


            }
        }



        public async Task <List<PatientDtoName>> GetAllName()
         => await _patientRepository.GetAllName(); 
        

        public async Task<List<BiologicalIndicatorDto>> GetBIByName(string name)
        {
            var BI=await _patientRepository.GetBIByName(name);
            var MappedBI = _mapper.Map<List<BiologicalIndicatorDto>>(BI);

            return  MappedBI;
           
        }

        public async Task<List<BiologicalIndicatorsDto2>>GetAllCritical()
        {
			 

			
			var criticalBI = await _BiologicalIndicatorsRepositoryRepository.GetAll();
            foreach (var BI in criticalBI)
            {
                _context.Entry(BI).Reference(n=>n.patient).Load(); 
            }


			
			var filter = criticalBI
				.Where(b => b.HealthCondition == "At Risk")
				.GroupBy(b => b.Date)
				.Select(g => new BiologicalIndicatorsDto2
				{
					Date = g.Key,
					Count = g.DistinctBy(p => p.PatientId).Count(),
					Patients = g.DistinctBy(p => p.PatientId)
								.Select(p => new PatientDtoName { Name = p.patient.Name,State = "At Risk" }) 
								.ToList()  
				})
				.ToList();

			return filter;

		}


    }
}
