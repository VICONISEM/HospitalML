using AutoMapper;
using Hospital.BLL.BiologicalIndicatorServices.Dto;
using Hospital.BLL.BiologicalIndicatorServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BiologicalIndicatorController : ControllerBase
    {
        private readonly IBiologicalIndicatorService _biologicalIndicatorService;

        private readonly IMapper _mapper;


        public BiologicalIndicatorController(IBiologicalIndicatorService Service, IMapper mapper)
        {
            _biologicalIndicatorService = Service;
            
            _mapper = mapper;
        }
        [HttpPost("Delete")]
        public async Task<ActionResult<int>>DeleteBio(int Id)
        {
            var Result = await _biologicalIndicatorService.GetById(Id);

            if(Result is not null)
            {
              
                return  Ok(await _biologicalIndicatorService.Delete(Result));

            }
            else
            {
                return BadRequest("Id Not Found");
            }


        }

      














    }



    }

