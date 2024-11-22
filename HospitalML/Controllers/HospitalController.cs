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
        public async Task<ActionResult<HospitalDto>> UpdateHospital( HospitalDto hospitalDto, int Id)
        {
            var IfNewPhoto = await HospitalRepo.GetById(Id);

            if (Id != 0 && Id != hospitalDto.Id) return BadRequest();
            var Url = hospitalDto.ImageURL.Substring(hospitalDto.ImageURL.IndexOf("/HospitalImages") + 1);



            if (IfNewPhoto.ImageURL.Substring(hospitalDto.ImageURL.IndexOf("/HospitalImages") + 1) != Url)
            {
                 ImageHandler.DeletePhoto(IfNewPhoto.ImageURL.Substring(hospitalDto.ImageURL.IndexOf("/HospitalImages") + 1));
                IfNewPhoto.ImageURL = ImageHandler.SavePhoto(hospitalDto.HospitalImage).Result;

            }
            mapper.Map(hospitalDto, IfNewPhoto);
           // IfNewPhoto.ImageURL = ImageHandler.SavePhoto(hospitalDto.HospitalImage).Result;


            var Result = await HospitalRepo.Update(IfNewPhoto);

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
