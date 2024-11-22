using AutoMapper;
using Hospital.BLL.HospitalServices.Dto;
using Hospital.BLL.HospitalServices.ImageHandler;
using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using static IronPython.Modules._ast;

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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HospitalDto>> AddHospital([FromForm] HospitalDto hospitalDto)
        {

            hospitalDto.ImageURL = await ImageHandler.SavePhoto(hospitalDto.HospitalImage);
            var hospital = mapper.Map<Hospitals>(hospitalDto);
            //Hospitals hospital = new Hospitals()
            //{
            //    Name = hospitalDto.Name,
            //    Address = hospitalDto.Address,
            //    City = hospitalDto.City,
            //    Country = hospitalDto.Country,
            //    ImageURL = hospitalDto.ImageURL
            //};

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
           await ImageHandler.DeletePhoto(hospital.ImageURL);
            var result = await HospitalRepo.Delete(hospital);

            if (result == 0) return BadRequest();

            return Ok();
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HospitalDto>> GetHospitalById(int? id)
        {
            if (!id.HasValue) return BadRequest("Invalid hospital ID.");

            var hospital = await HospitalRepo.GetById(id.Value);
            var Dto = new HospitalDto()
            {
                Address = hospital.Address,
                Name = hospital.Name,
                City = hospital.City,
                Country = hospital.Country,
                Id = hospital.Id,
                ImageURL = $"{Request.Scheme}://{Request.Host}/{hospital.ImageURL}"
            };

          

            if (hospital == null) return NotFound();

            return Ok(Dto);
        }

        [HttpPost("UpdateHospital/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<HospitalDto>> UpdateHospital(HospitalDto hospitalDto, int Id)
        {
            if (Id != hospitalDto.Id)
                return BadRequest("The provided ID does not match the hospital's ID.");

            var hospitalToUpdate = await HospitalRepo.GetById(Id);

            if (hospitalToUpdate == null)
                return NotFound($"Hospital with ID {Id} not found.");

            try
            {
                if (hospitalDto.HospitalImage != null)
                {
                    if (!string.IsNullOrEmpty(hospitalToUpdate.ImageURL) &&
                        hospitalToUpdate.ImageURL != hospitalDto.ImageURL)
                    {
                        ImageHandler.DeletePhoto(hospitalToUpdate.ImageURL);
                    }

                    hospitalToUpdate.ImageURL = await ImageHandler.SavePhoto(hospitalDto.HospitalImage);
                }

                mapper.Map(hospitalDto, hospitalToUpdate);

                var result = await HospitalRepo.Update(hospitalToUpdate);

                if (result == 0)
                    return StatusCode(500, "Failed to update hospital in the database.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while updating the hospital: {ex.Message}");
            }

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
                //var hospitalDto = mapper.Map<HospitalDto>(hospital);
                var hospitalDto = new HospitalDto()
                {
                    Address = hospital.Address,
                    Name = hospital.Name,
                    City = hospital.City,
                    Country = hospital.Country,
                    Id = hospital.Id,
                    ImageURL = $"{Request.Scheme}://{Request.Host}/{hospital.ImageURL}"
                };
                Result.Add(hospitalDto);
            }

            return Ok(Result);
        }

    }
}
