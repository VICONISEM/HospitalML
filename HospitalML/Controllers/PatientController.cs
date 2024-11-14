using AutoMapper;
using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.BiologicalIndicatorServices.Service;
using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.PatientServices.Service;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _IPatientService;
        private readonly IBiologicalIndicatorService _IBiologicalIndicatorService;
        private readonly IMapper mapper;
        private readonly IGenaricRepository<Patient> PatientRepo;

        public PatientController(IPatientService PatientService, IBiologicalIndicatorService biologicalIndicatorService, IMapper mapper, IGenaricRepository<Patient> PatientRepo)
        {
            _IPatientService = PatientService;
            _IBiologicalIndicatorService = biologicalIndicatorService;
            this.mapper = mapper;
            this.PatientRepo = PatientRepo;
        }

        [HttpGet("AllNames")]
        public async Task<ActionResult<List<PatientDtoName>>> GetAllNames()
        {
            var Names = await _IPatientService.GetAllName();
            return Ok(Names);
        }

        [HttpGet("{Name}")]
        public async Task<ActionResult<List<BiologicalIndicatorDto>>> GetBIByName(string Name)
        {
            var Result = await _IPatientService.GetBIByName(Name);
            return Ok(Result);
        }

        [HttpGet("{Name}/{D1}/{D2}")]
        public async Task<ActionResult<List<BiologicalIndicatorDto>>> GetBIByNameFilter(string Name, DateOnly D1, DateOnly D2)
        {
            if (D1 <= D2)
            {
                var Result = await _IPatientService.GetBIByName(Name);
                var Filter = Result.Where(R => R.date >= D1 && R.date <= D2);
                return Ok(Filter);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetAllBiologicalIndicator")]
        public async Task<ActionResult<List<BiologicalIndicatorDto>>> GetAllBiologicalIndicator()
        {
            var Result = await _IBiologicalIndicatorService.GetAllBiologicalIndicators();
            return Ok(Result);
        }

        [HttpGet("GetAllCritical")]
        public async Task<ActionResult<List<BiologicalIndicatorsDto2>>> GetAllCritical()
        {
            var Result = await _IPatientService.GetAllCritical();
            return Ok(Result);
        }

        [HttpPost("CreatePatient")]
        public async Task<ActionResult<PatientDto>> CreatePatient(PatientDto patientDto)
        {
            var patient = mapper.Map<Patient>(patientDto);

            var Result = await PatientRepo.Add(patient);

            if (Result == 0) return BadRequest();

            return Ok(patient);
        }

        [HttpPost("UpdatePatient")]
        public async Task<ActionResult<PatientDto>> UpdatePatient(PatientDto patientDto)
        {

            var PatientExist = await PatientRepo.GetById(patientDto.Id);
            if(PatientExist == null) return BadRequest();

            var patient = mapper.Map<Patient>(patientDto);

            var Result = await PatientRepo.Update(patient);

            if(Result == 0) return BadRequest(); 

            var patientRes = mapper.Map<PatientDto>(patient);

            return Ok(patient);
        }

        [HttpPost("DeletePatient/{Id}")]
        public async Task<ActionResult> DeletePatient(int Id)
        {
            var patient = await PatientRepo.GetById(Id);

            if (patient == null) return BadRequest(); 

            var Result = await PatientRepo.Delete(patient);

            if (Result == 0) return BadRequest();

            return Ok();
        }

        [HttpPost("AddBio")]
        public async Task<ActionResult> AddBio(BiologicalIndicatorDto indicatorDto, int UserId)
        {
            var Bio = mapper.Map<BiologicalIndicators>(indicatorDto);

            var user = await PatientRepo.GetById(UserId);

            if (user == null) return BadRequest();

            user.biologicalIndicators.Add(Bio);

            var Result = await PatientRepo.Update(user);

            if(Result == 0)  return BadRequest(); 

            return Ok();
            
        }
    }
}
