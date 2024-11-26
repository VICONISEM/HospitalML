using AutoMapper;
using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.BiologicalIndicatorServices.Service;
using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.PatientServices.Service;
using Hospital.BLL.Repository;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Contexts;
using Hospital.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GraphController : ControllerBase
    {
        private readonly IPatientService _IPatientService;
        private readonly IBiologicalIndicatorService _IBiologicalIndicatorService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HospitalDbContext context;
        private readonly IMapper mapper;
        private readonly IGenaricRepository<Patient> patientRepository;

        public GraphController(IPatientService PatientService, IBiologicalIndicatorService biologicalIndicatorService, UserManager<ApplicationUser> userManager,HospitalDbContext context, IMapper mapper,IGenaricRepository<Patient> patientRepository)
        {
            _IPatientService = PatientService;
            _IBiologicalIndicatorService = biologicalIndicatorService;
            _userManager = userManager;
            this.context = context;
            this.mapper = mapper;
            this.patientRepository = patientRepository;
        }

        [HttpGet("AllNames")]
        public async Task<ActionResult<List<PatientDtoName>>> GetAllNames()
        {
            var Role = User.FindFirstValue(ClaimTypes.Role);
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(Email);

            var Names = new List<PatientDtoName>();

            if (Role == "Admin")
            {
                Names = await _IPatientService.GetAllName();
            }
            else
            {
                Names = _IPatientService.GetAllName().Result.Where(p => p.HospitalId == user.HospitalId).ToList();
            }

            return Ok(Names);
        }

        [HttpGet("{Name}")]
        public async Task<ActionResult<List<BiologicalIndicatorDto>>> GetBIByName(string Name)
        {
            var Role = User.FindFirstValue(ClaimTypes.Role);
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);
            var Result = new List<BiologicalIndicatorDto>();

            if (Role == "Admin")
            {
                Result = await _IPatientService.GetBIByName(Name);
            }

            else
            {

                var filter = (await patientRepository.GetAll())
                  .Where(p => p.Name.Trim().ToLower() == Name.Trim().ToLower())
                  .ToList();


                foreach (var patient in filter)
                {
                    context.Entry(patient).Collection(p => p.biologicalIndicators).Load();
                }

                var test = filter
                                .Where(p => p.HospitalId == user.HospitalId)
                                .SelectMany(p => p.biologicalIndicators)
                                .ToList();

                Result = mapper.Map<List<BiologicalIndicatorDto>>(test);
            }

            return Ok(Result);
        }

        [HttpGet("{Name}/{D1}/{D2}")]
        public async Task<ActionResult<List<BiologicalIndicatorDto>>> GetBIByNameFilter(string Name, DateOnly D1, DateOnly D2)
        {
            var Role = User.FindFirstValue(ClaimTypes.Role);
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);
            var Result = new List<BiologicalIndicatorDto>();
            var Filter = new List<BiologicalIndicatorDto>();

            if (D1 <= D2)
            {
                if (Role == "Admin")
                {
                    Result = await _IPatientService.GetBIByName(Name);
                    Filter = Result.Where(R => R.date >= D1 && R.date <= D2).ToList();
                }

                else
                {
                    var filter = (await patientRepository.GetAll())
                 .Where(p => p.Name.Trim().ToLower() == Name.Trim().ToLower())
                 .ToList();


                    foreach (var patient in filter)
                    {
                        context.Entry(patient).Collection(p => p.biologicalIndicators).Load();
                    }

                    var test = filter
                                    .Where(p => p.HospitalId == user.HospitalId)
                                    .SelectMany(p => p.biologicalIndicators)
                    .ToList();

                    Filter = mapper.Map<List<BiologicalIndicatorDto>>(test.Where(R => R.Date >= D1 && R.Date <= D2).ToList());
                }

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

            var Role = User.FindFirstValue(ClaimTypes.Role);
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);
            if (Role == "Admin")
            {

                var Result = await _IPatientService.GetAllCritical();
                return Ok(Result);
            }
            else
            {
                var Result = (await _IPatientService.GetAllCritical()).Select(report => new BiologicalIndicatorsDto2 { Count = report.Patients.Where(P => P.HospitalId == user.HospitalId).Count(), Date = report.Date, Patients = report.Patients.Where(p => p.HospitalId == user.HospitalId).Select(p => new PatientDtoName() { Id = p.Id, Name = p.Name, LastBiologicalIndicator = p.LastBiologicalIndicator, HospitalId = p.HospitalId }).ToList() });
                return Ok(Result);
            }


        }
    }
}




