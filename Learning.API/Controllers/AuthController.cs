using Learning.API.Models.DTO;
using Learning.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Learning.API.Controllers
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
        //post:
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {

                // Add roles to this User
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }
            return BadRequest("something went wrong");
        }

        //POST: /api/Auth/login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
                if (user != null)
                {
                    var checkpasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                    if (checkpasswordResult)
                    {
                        //Get roles for this user
                        var roles = await userManager.GetRolesAsync(user);

                        if (roles != null)
                        {
                            //Create Token
                            var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                            var response = new LoginResponseDto
                            {
                                JwtToken = jwtToken
                            };
                            return Ok(response);
                        }
                        return Ok();
                    }

                }
                return BadRequest("Username or password incorrect.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}

