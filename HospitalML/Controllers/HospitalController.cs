using AutoMapper;
using Hospital.BLL.HospitalServices.Dto;
using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class HospitalController : ControllerBase
    {
        private readonly IGenaricRepository<Patient> PatientRepo;
        private readonly IGenaricRepository<Hospitals> HospitalRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public HospitalController(IGenaricRepository<Patient> PatientRepo, IGenaricRepository<Hospitals> HospitalRepo, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.PatientRepo = PatientRepo;
            this.HospitalRepo = HospitalRepo;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost("AddHospital")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<HospitalDto>> AddHospital(HospitalDto hospitalDto)
        {
            Hospitals hospital = new Hospitals()
            {
                Name = hospitalDto.Name,
                Address = hospitalDto.Address,
                City = hospitalDto.City,
                Country = hospitalDto.Country
            };

            var Result = await HospitalRepo.Add(hospital);

            if (Result == 0) return BadRequest();

            return hospitalDto;
        }

        [HttpPost("DeleteHospital/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteHospital(int? id)
        {
            if (!id.HasValue) return BadRequest("Invalid hospital ID.");

            Hospitals? hospital = await HospitalRepo.GetById(id.Value);

            if (hospital == null) return NotFound();

            if (hospital.ApplicationUser != null)
            {
                hospital.ApplicationUser.HospitalId = null;
                await HospitalRepo.Update(hospital);
            }

            var result = await HospitalRepo.Delete(hospital);

            if (result == 0) return BadRequest();

            return Ok();
        }

        [HttpPost("UpdateHospital/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HospitalDto>> UpdateHospital( HospitalDto hospitalDto, int Id)
        {
            if (Id != 0 && Id != hospitalDto.Id) return BadRequest();

            var hospital = mapper.Map<Hospitals>(hospitalDto);

            var Result = await HospitalRepo.Update(hospital);

            if (Result == 0) return BadRequest();

            return Ok(hospitalDto);
        }

        [HttpGet("GetHospitals")]
        [Authorize]
        public async Task<ActionResult<List<HospitalDto>>> GetAllHospitals()
        {
            var hospitals = await HospitalRepo.GetAll();
            var Result = new List<HospitalDto>();
            var Role = User.FindFirstValue(ClaimTypes.Role);
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(Email);

            if(Role!="Admin")
            {
                hospitals = hospitals.Where(H => H.Id == user.HospitalId).ToList();
            }

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
