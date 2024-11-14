using Hospital.BLL.Repository;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Entities;
using HospitalML.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IGenaricRepository<Patient> PatientRepo;
        private readonly IGenaricRepository<Hospitals> HospitalRepo;

        public DashboardController(IGenaricRepository<Patient> PatientRepo, IGenaricRepository<Hospitals> HospitalRepo)
        {
            this.PatientRepo = PatientRepo;
            this.HospitalRepo = HospitalRepo;
        }

        [HttpPost("AddHospital")]
        public async Task<ActionResult<HospitalDto>> AddHospital(HospitalDto hospitalDto)
        {
            Hospitals hospital = new Hospitals()
            {
                Name = hospitalDto.Name,
                Address = hospitalDto.Address,
                City = hospitalDto.City,
                Country = hospitalDto.Country
            };

            await HospitalRepo.Add(hospital);

            return hospitalDto;
        }

        [HttpPost("DeleteHospital")]
        public async Task<ActionResult> DeleteHospital(int id)
        {
            Hospitals? hospital = await HospitalRepo.GetById(id);

            if (hospital == null) return BadRequest();

            await HospitalRepo.Delete(hospital);
        }

    }
}
