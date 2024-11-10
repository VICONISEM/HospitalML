using Hospital.DAL.Contexts;
using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.Repository
{
    public class BiologicalIndicatorsRepository : GenericRepository<BiologicalIndicators>
    {
		private readonly HospitalDbContext _context;

		public BiologicalIndicatorsRepository(HospitalDbContext context ):base(context)
        {
			_context = context;
		}
    }
}
