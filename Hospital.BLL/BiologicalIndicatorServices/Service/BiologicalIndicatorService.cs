using AutoMapper;
using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.Repository;
using Hospital.DAL.Contexts;
using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.BiologicalIndicatorServices.Service
{
	public class BiologicalIndicatorService : IBiologicalIndicatorService
	{
		private readonly HospitalDbContext _context;
		private readonly IMapper _mapper;

		public BiologicalIndicatorsRepository _BiologicalIndicatorsRepository
		{

			get
			{
				return new BiologicalIndicatorsRepository(_context);
			}
		
		
		}	 
			 

		public BiologicalIndicatorService( HospitalDbContext context,IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}
        public async Task<List<BiologicalIndicatorDto>> GetAllBiologicalIndicators()
		{
			var BI = await _BiologicalIndicatorsRepository.GetAll();
			var MappedBI=_mapper.Map<List<BiologicalIndicatorDto>>(BI);	
			return MappedBI;
			 
		}

        public async Task<List<BiologicalIndicatorDto>> GetAll()
        {
            var BI = await _BiologicalIndicatorsRepository.GetAll();
            var MappedBI = _mapper.Map<List<BiologicalIndicatorDto>>(BI);
            return MappedBI;

        }

        public async Task<BiologicalIndicatorDto?> GetById(int? id)
        {
           var BI= await _BiologicalIndicatorsRepository.GetById(id);
            var MappedBI = _mapper.Map<BiologicalIndicatorDto>(BI);
            return MappedBI;
        }

        public async Task<int> Add(BiologicalIndicatorDto entity)
        {
           var BI=_mapper.Map<BiologicalIndicators>(entity);
           return  await _BiologicalIndicatorsRepository.Add(BI);
        }

        public async Task<int> Update(BiologicalIndicatorDto entity)
        {
            var BI = _mapper.Map<BiologicalIndicators>(entity);

            return await _BiologicalIndicatorsRepository.Update(BI);
        }

        public async Task<int> Delete(BiologicalIndicatorDto entity)
        {
            var BI = _mapper.Map<BiologicalIndicators>(entity);
            return await _BiologicalIndicatorsRepository.Delete(BI);
        }
    }
}
