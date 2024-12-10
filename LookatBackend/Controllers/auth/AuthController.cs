using LookatBackend.Dtos.Barangay;
using LookatBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LookatBackend.Services.AuthService;
using LookatBackend.Interfaces;
using LookatBackend.Dtos.Barangay.BarangayLoginDto;


namespace LookatBackend.Controllers.AuthController
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IBarangayRepository _barangayRepository;
        private readonly AuthService _authService;

        public AuthController(IBarangayRepository barangayRepository, AuthService authService)
        {
            _barangayRepository = barangayRepository;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid login request");
            }
            try
            {
                var loginResponse = await _authService.LoginAsync(loginDto);


                return Ok(new 
                { 
                    Token = loginResponse.Token, 
                    BarangayId = loginResponse.BarangayId 
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid barangay name or password");
            }
        }
    }
}