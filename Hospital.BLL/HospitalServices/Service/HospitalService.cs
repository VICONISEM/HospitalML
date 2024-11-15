using AutoMapper;
using Hospital.BLL.HospitalServices.Dto;
using Hospital.BLL.Repository;
using Hospital.DAL.Contexts;
using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.HospitalServices.Service
{
    public class HospitalService : IHospitalService
    {

        public HospitalService(HospitalDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper=mapper;
        }


        private readonly IMapper _mapper;

       
        private readonly HospitalDbContext _context;
        public HospitalRepository HospitalRepository {

            get
            {
                return  new HospitalRepository(_context);
            }
        
        }

        public async Task<int> Add(HospitalDto entity)
        {
            var Hospital=_mapper.Map<Hospitals>(entity);
            return await HospitalRepository.Add(Hospital);
            
        }

        public async Task<int> Delete(HospitalDto entity)
        {
            var Hospital = await HospitalRepository.GetById(entity.Id);

            return await  HospitalRepository.Delete(Hospital);



        }

        public async  Task<List<HospitalDto>> GetAll()
        {
            var Hospitals=await HospitalRepository.GetAll();
            var MappedHospitals = _mapper.Map<List<HospitalDto>>(Hospitals);
            return MappedHospitals;
        }

        public async Task<HospitalDto?> GetById(int? id)
        {
           var Hospital=await HospitalRepository.GetById(id);
           var MappedHospital = _mapper.Map<HospitalDto>(Hospital);
           return MappedHospital;

        }

        public async Task<int> Update(HospitalDto entity)
        {
            var Hospital = await HospitalRepository.GetById(entity.Id);
            var Mapped = _mapper.Map<Hospitals>(entity);
            Hospital.Address= entity.Address;
            Hospital.Name = entity.Name;
            Hospital.City= entity.City;
            Hospital.Country= entity.Country;
            return await HospitalRepository.Update(Hospital);
        }

       

       
       

     
    }
}
