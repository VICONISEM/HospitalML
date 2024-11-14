using Hospital.BLL.HospitalServices.Dto;
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
    public class HospitalController : ControllerBase
    {
        private readonly IGenaricRepository<Patient> PatientRepo;
        private readonly IGenaricRepository<Hospitals> HospitalRepo;

        public HospitalController(IGenaricRepository<Patient> PatientRepo, IGenaricRepository<Hospitals> HospitalRepo)
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

            var result = await HospitalRepo.Delete(hospital);

            if(result == 0) return BadRequest();

            return Ok();
        }

        [HttpPost("UpdateHospital")]
        public async Task<ActionResult<HospitalDto>> UpdateHospital(HospitalDto hospitalDto, int Id)
        {
            var hospital = new Hospitals()
            {
                Name = hospitalDto.Name,
                Address = hospitalDto.Address,
                City = hospitalDto.City,
                Country = hospitalDto.Country,
                Id = Id
            };

            var Result = await HospitalRepo.Update(hospital);

            if (Result == 0) return BadRequest();



            return Ok(hospitalDto);
        }

        [HttpGet("GetHospitals")]
        public async Task<ActionResult<List<HospitalDto>>> GetAllHospitals()
        {
            var hospitals = await HospitalRepo.GetAll();

            var Result = new List<HospitalDto>();

            foreach (var hospital in hospitals)
            {
                var hospitalDto = new HospitalDto()
                {
                    Address = hospital.Address,
                    Name = hospital.Name,
                    City = hospital.City,
                    Country = hospital.Country,
                    Id = hospital.Id
                };
                Result.Add(hospitalDto);
            }

            return Ok(Result);
        }

    }
}
