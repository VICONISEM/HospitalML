using Hospital.BLL.HospitalServices.Dto;
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

        public HospitalController(IGenaricRepository<Patient> PatientRepo, IGenaricRepository<Hospitals> HospitalRepo, UserManager<ApplicationUser> userManager)
        {
            this.PatientRepo = PatientRepo;
            this.HospitalRepo = HospitalRepo;
            this.userManager = userManager;
        }

        [HttpPost("AddHospital")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<HospitalDto>> AddHospital( [FromForm] HospitalDto hospitalDto)
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

        [HttpPost("DeleteHospital/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteHospital([FromForm] int id)
        {
            Hospitals? hospital = await HospitalRepo.GetById(id);

            if (hospital == null) return BadRequest();

            var result = await HospitalRepo.Delete(hospital);

            if(result == 0) return BadRequest();

            return Ok();
        }

        [HttpPost("UpdateHospital/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HospitalDto>> UpdateHospital( HospitalDto hospitalDto, int Id)
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
