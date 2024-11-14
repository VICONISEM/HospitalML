using Hospital.BLL.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository patientRepo;

        public PatientController(IPatientRepository PatientRepo)
        {
            patientRepo = PatientRepo;
        }

        //[HttpGet("GetAllPatients")]

    }
}
