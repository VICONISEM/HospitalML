using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.BiologicalIndicatorServices.Service;
using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.PatientServices.Service;
using Hospital.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _IPatientService;
        private readonly IBiologicalIndicatorService _IBiologicalIndicatorService;

        public PatientController(IPatientService PatientService, IBiologicalIndicatorService biologicalIndicatorService)
        {
            _IPatientService = PatientService;
            _IBiologicalIndicatorService = biologicalIndicatorService;
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
            var patient = new Patient()
            {
                Name = patientDto.Name,
                PhoneNumber = patientDto.PhoneNumber,
                Address = patientDto.Address,
                Sex = patientDto.Sex,
                NumberOfBirth = patientDto.NumberOfBirth,
                Pregnant = patientDto.Pregnant,
                HospitalId = patientDto.HospitalId

    };
        }

    }
}
