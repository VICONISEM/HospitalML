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

            var Token = await tokenServices.CreateTokenAsync(user, userManager);

            var userDto = new UserDto()
            {
                Email = loginUser.Email,
                Username = user.UserName,
                Token = Token,
                HospitalName = _HospitalRepo.GetById(user.HospitalId).Result.Name,
                Role = (await userManager.GetRolesAsync(user))[0],
            };

            return Ok(userDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDto>> CreateUser(SignUpUserDto signUpUserDto)
        {

            var Flag = await userManager.FindByEmailAsync(signUpUserDto.Email);
            if(Flag is not null)
            {
                return BadRequest("There is User With Same Email");
            }

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
                Token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "")
            };

            return Ok(userDto);
        }

        //[Authorize(Roles = "Admin")]
        //[HttpGet("GetAllUsers")]
        //public async Task<ActionResult<List<UserDto>>> GetUsersAsync()
        //{
        //    var users = userManager.Users.ToList();

        //    var Result = new List<UserDto>();

        //    foreach (var user in users)
        //    {
        //        Result.Add(new UserDto()
        //        {
        //            Email = user.Email,
        //            Username = user.UserName,
        //            HospitalName = (await _HospitalRepo.GetById(user.HospitalId))?.Name ?? "No Hopital",
        //            Role = (await userManager.GetRolesAsync(user))[0],
        //            Token = "Dummy Token"
        //        });
        //    }

        //    return Result;
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpGet("GetByEmail/{Email}")]
        //public async Task<ActionResult<UserDto>> GetUserByEmail(string Email)
        //{
        //    var user = await userManager.FindByEmailAsync(Email);

        //    if (user == null) return NotFound();

        //    return Ok(new UserDto()
        //    {
        //        Email = Email,
        //        Username = user.UserName,
        //        HospitalName = (await _HospitalRepo.GetById(user.HospitalId))?.Name ?? "No Hospital",
        //        Token = "Dummy Token",
        //        Role = (await userManager.GetRolesAsync(user))[0]
        //    });
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpGet("Delete/{Email}")]
        //public async Task<ActionResult> Delete(string Email)
        //{
            
        //    var user = await userManager.FindByEmailAsync(Email);

        //    if(user is null) return NotFound();

        //    var result = await userManager.DeleteAsync(user);

        //    if (!result.Succeeded) return BadRequest();

        //    return Ok();
        //}
    }
}
