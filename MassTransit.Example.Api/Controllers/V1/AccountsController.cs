using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MassTransit.Example.Authentication.Configuration;
using MassTransit.Example.Authentication.Models.Dto.Incoming;
using MassTransit.Example.Authentication.Models.Dto.Outgoing;
using MassTransit.Example.DataService.IConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MassTransit.Example.Api.Controllers.V1
{
    public class AccountsController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AccountsController(
            IUnitOfWork unitOfWork, 
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor) : base(unitOfWork)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(
            [FromBody] UserRegistrationRequestDto registrationRequest
        )
        {
            if (ModelState.IsValid)
            {
                // Make sure the user's email does not already exists.
                var userWithThatEmail = await _userManager.FindByEmailAsync(registrationRequest.Email);

                if (userWithThatEmail != null)
                {
                    return BadRequest(new UserRegistrationResponseDto(){
                        Success = false,
                        Errors = new List<string>() {"Email already in use"}
                    });
                }

                // Now we can create the user.
                var newUser = new IdentityUser()
                {
                    Email = registrationRequest.Email,
                    UserName = registrationRequest.Username,
                    EmailConfirmed = true // TODO: Confirm the email!
                };

                var userCreationResult = await _userManager.CreateAsync(newUser, registrationRequest.Password);

                if (!userCreationResult.Succeeded)
                {
                    return BadRequest(new UserRegistrationResponseDto()
                    {
                        Success = false,
                        Errors = userCreationResult.Errors.Select(x => x.Description).ToList()
                    });
                }

                // Create JWT Token
                var token = GenerateJwtToken(newUser);

                // Finish process
                return Ok(new UserRegistrationResponseDto()
                {
                    Success = true,
                    Token = token
                });
            }
            else
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Errors = new List<string>() {"Invalid Payload"}
                });
            }
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            if (_jwtConfig.Secret == null)
            {
                throw new InvalidOperationException("Secret JWT token was null");
            }

            var jwtHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3), // TODO: Update the expiration to minutes when you have refresh token
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtHandler.WriteToken(token);

            return jwtToken;
        }
    }
}