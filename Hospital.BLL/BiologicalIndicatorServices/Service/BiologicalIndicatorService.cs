using AutoMapper;
using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.Repository;
using Hospital.DAL.Contexts;
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
	}
}
