using Hospital.BLL.PatientServices.Dto;
using Hospital.BLL.PatientServices.Service;
using Hospital.BLL.Repository.Interface;
using Hospital.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly IPatientService _repository;

        public GraphController(IPatientService repository)
        {
            _repository = repository;
        }

        [HttpGet("AllNames")]
        public ActionResult<List<string>>GetAllNames()
        {
            var Names = _repository.GetAllName();
            return Ok(Names);
        }

        [HttpGet("{Name}")]
        public ActionResult<List<PatientDto>>GetBIByName(string Name)
        {
            var Result = _repository.GetBIByName(Name);
            return Ok(Result);
        }
    }
}
