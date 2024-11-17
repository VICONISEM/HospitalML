using Hospital.BLL.Repository.Interface;
using Hospital.BLL.TokenServices;
using Hospital.DAL.Entities;
using HospitalML.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenServices tokenServices;

        private readonly IGenaricRepository<Hospitals> _HospitalRepo;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenServices tokenServices, IGenaricRepository<Hospitals> HospitalRepo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenServices = tokenServices;
            this._HospitalRepo = HospitalRepo;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginUserDto loginUser)
        {
            var user = await userManager.FindByEmailAsync(loginUser.Email);

            if(user is null)
                return Unauthorized(); 

            var Result = await signInManager.CheckPasswordSignInAsync(user,loginUser.Password, false);

            if (!Result.Succeeded) return Unauthorized();

            var userDto = new UserDto()
            {
                Email = loginUser.Email,
                Username = user.UserName,
                Token = await tokenServices.CreateTokenAsync(user, userManager),
                HospitalName = _HospitalRepo.GetById(user.HospitalId).Result.Name

            };
            return Ok(userDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDto>> CreateUser(SignUpUserDto signUpUserDto)
        {
            var user = new ApplicationUser()
            {
                Email = signUpUserDto.Email,
                UserName = signUpUserDto.Email.Split('@')[0],
                PhoneNumber = signUpUserDto.PhoneNumber,
                HospitalId = signUpUserDto.HospitalId
            };

            var Result = await userManager.CreateAsync(user, signUpUserDto.Password);

            if(!Result.Succeeded) return BadRequest();

            var userToAddRole = await userManager.FindByEmailAsync(signUpUserDto.Email);
            await userManager.AddToRoleAsync(userToAddRole, "User");

            var userDto = new UserDto()
            {
                Email = signUpUserDto.Email,
                Username = signUpUserDto.Username,
                HospitalName = _HospitalRepo.GetById(signUpUserDto.HospitalId).Result.Name,
                Token = await tokenServices.CreateTokenAsync(user,userManager)
            };

            return userDto;
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(Email);

            if (user == null) return NotFound();

            var userDto = new UserDto()
            {
                Email = user.Email,
                Username = user.UserName,
                HospitalName = _HospitalRepo.GetById(user.HospitalId).Result.Name,
                Token =Request.Headers["Authorization"].ToString().Replace("Bearer ", "")
            };

            return Ok(userDto);
        }

    }
}
