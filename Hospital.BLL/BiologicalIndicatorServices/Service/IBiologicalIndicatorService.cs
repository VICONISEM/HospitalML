using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.BiologicalIndicatorServices.Service
{
	public interface IBiologicalIndicatorService
	{
        public BiologicalIndicatorsRepository _BiologicalIndicatorsRepository { get; }

		Task<List<BiologicalIndicatorDto>> GetAllBiologicalIndicators();
    }
}
