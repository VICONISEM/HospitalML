using Hospital.BLL.HospitalServices.Dto;
using Hospital.BLL.Repository;
using Hospital.BLL.Repository.Interface;
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

        public HospitalRepository HospitalRepository { get; }
        Task<List<HospitalDto>> GetAll();

        Task<HospitalDto?> GetById(int? id);

        Task<int> Add(HospitalDto entity);

        Task<int> Update(HospitalDto entity);
        Task<int> Delete(HospitalDto entity);

    }
}
