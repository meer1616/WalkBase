using Authentication.Contracts;
using Authentication.Models.DTO;
using Authentication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

           var identityResult = await userManager.CreateAsync(identityUser,registerRequestDto.Password);

            if (identityResult.Succeeded){

                if(registerRequestDto.Roles!= null && registerRequestDto.Roles.Any())
                {
                    // Add user to roles
                    identityResult=await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User registered successfully with roles.");
                    }
                }
            }
            var error = new Error
            {
                StatusCode = StatusCodes.Status400BadRequest.ToString(),
                Message = "Something went wrong while logging in the user."
            };
            return BadRequest(error);

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var identityUser = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (identityUser == null)
            {
                return Unauthorized("Invalid username");
            }
            var isPasswordValid = await userManager.CheckPasswordAsync(identityUser, loginRequestDto.Password);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid password.");
            }
            // Here you would typically generate a JWT token and return it
            // For simplicity, we are just returning a success message
             var roles=await userManager.GetRolesAsync(identityUser);
            if (roles != null)
            {
                var jwtToken= tokenRepository.CreateJWTToken(identityUser, roles.ToList());
           
                var response =new LoginResponseDto
                {
                    jwtToken= jwtToken,
                    user=identityUser
                };
                return Ok(response);
            }
            var error = new Error
            {
                StatusCode = StatusCodes.Status400BadRequest.ToString(),
                Message = "Something went wrong while logging in the user."
            };
            return BadRequest(error);
        }

    }
}
