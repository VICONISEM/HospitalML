using AutoMapper;
using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.BiologicalIndicatorServices.Service;
using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.PatientServices.Service;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Entities;
using HospitalML.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

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
        private readonly HttpClient client;


        public PatientController(IPatientService PatientService, IBiologicalIndicatorService biologicalIndicatorService, IMapper mapper, IGenaricRepository<Patient> PatientRepo, HttpClient client)
        {
            _IPatientService = PatientService;
            _IBiologicalIndicatorService = biologicalIndicatorService;
            this.mapper = mapper;
            this.PatientRepo = PatientRepo;
            this.client = client;
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

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(new { sugarPercentage = indicatorDto.SugarPercentage, bloodPressure = indicatorDto.BloodPressure, averageTemprature = indicatorDto.AverageTemprature}), System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api-model-kohl.vercel.app/predict", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<ExternalAPIResponse>(responseContent);
                    indicatorDto.HealthConditionScore = (int) data.predictedHealthConditionScore;
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            if (indicatorDto.BloodPressure > 140 || indicatorDto.SugarPercentage > 125) indicatorDto.HealthCondition = "At Risk";
            else if (indicatorDto.HealthConditionScore >= 80) indicatorDto.HealthCondition = "Healthy";
            else if (indicatorDto.HealthConditionScore >= 60) indicatorDto.HealthCondition = "Moderate";
            else indicatorDto.HealthCondition = "Unhealthy";

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
