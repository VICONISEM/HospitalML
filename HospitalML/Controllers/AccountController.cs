using Hospital.BLL.TokenServices;
using HospitalML.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ITokenServices tokenServices;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenServices tokenServices)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenServices = tokenServices;
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
                Token = await tokenServices.CreateTokenAsync(user,userManager)
            };

            return Ok(userDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDto>> CreateUser(SignUpUserDto signUpUserDto)
        {
            var user = new IdentityUser()
            {
                Email = signUpUserDto.Email,
                UserName = signUpUserDto.Username,
                PhoneNumber = signUpUserDto.PhoneNumber
            };

            var Result = await userManager.CreateAsync(user, signUpUserDto.Password);

            if(!Result.Succeeded) return BadRequest();

            var userToAddRole = await userManager.FindByEmailAsync(signUpUserDto.Email);
            await userManager.AddToRoleAsync(userToAddRole, "User");

            var userDto = new UserDto()
            {
                Email = signUpUserDto.Email,
                Username = signUpUserDto.Username,
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
                Username = user.UserName
            };

            return Ok(userDto);
        }

    }
}
