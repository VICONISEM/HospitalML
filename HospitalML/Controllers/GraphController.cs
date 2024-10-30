﻿using Hospital.BLL.Patient.Dto;
using Hospital.BLL.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly IPatientRepository _repository;

        public GraphController(IPatientRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
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
