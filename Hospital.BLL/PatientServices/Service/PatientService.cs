﻿using AutoMapper;
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



        public async Task <List<PatientDtoName>> GetAllName()
         => await _patientRepository.GetAllName(); 
        

        public async Task<List<PatientDto>> GetBIByName(string name)
        {
            var BI=await _patientRepository.GetBIByName(name);
            var MappedBI = _mapper.Map<List<PatientDto>>(BI);

            return  MappedBI;
           
        }
    }
}
