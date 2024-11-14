using Hospital.BLL.HospitalServices.Dto;
using Hospital.BLL.Repository;
using Hospital.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.HospitalServices.Service
{
    public class HospitalService : IHospitalService
    {


        public HospitalService(HospitalDbContext context ) {

            _context = context;
        }
        private readonly HospitalDbContext _context;
        public HospitalRepository HospitalRepository {

            get
            {
                return  new HospitalRepository(_context);
            }
        
        }

        public async Task Add(HospitalDto entity)
        {
            
        }

        public Task Delete(HospitalDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<HospitalDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<HospitalDto?> GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task Update(HospitalDto entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IHospitalService.Add(HospitalDto entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IHospitalService.Update(HospitalDto entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IHospitalService.Delete(HospitalDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
