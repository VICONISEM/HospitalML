﻿using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.BiologicalIndicatorServices.Service;
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
        private readonly IPatientService _IPatientService;
        private readonly IBiologicalIndicatorService _IBiologicalIndicatorService;


		public GraphController(IPatientService PatientService,IBiologicalIndicatorService biologicalIndicatorService)
        {
			_IPatientService = PatientService;
			_IBiologicalIndicatorService= biologicalIndicatorService;

		}

        [HttpGet("AllNames")]
        public ActionResult<List<string>>GetAllNames()
        {
            var Names = _IPatientService.GetAllName();
            return Ok(Names);
        }

        [HttpGet("{Name}")]
        public ActionResult<List<PatientDto>>GetBIByName(string Name)
        {
            var Result = _IPatientService.GetBIByName(Name);
            return Ok(Result);
        }

        [HttpGet("GetAllBiologicalIndicator")]
        public ActionResult<List<BiologicalIndicatorDto>> GetAllBiologicalIndicator()
        {
            var Result= _IBiologicalIndicatorService.GetAllBiologicalIndicators();
            return Ok(Result);
        }


	}
}
