using Hospital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.HospitalServices.Service
{
    public interface IHospitalService
    {
        Task<List<Hospitals>> GetAll();

        Task<Hospitals?> GetById(int? id);

        Task Add(Hospitals entity);

        Task Update(Hospitals entity);
        Task Delete(Hospitals entity);
    }
}
