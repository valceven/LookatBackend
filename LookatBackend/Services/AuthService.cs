using LookatBackend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LookatBackend.Interfaces;
using LookatBackend.Dtos.Barangay.BarangayLoginDto;
using LookatBackend.Dtos.Barangay.LoginResponseDto;



namespace LookatBackend.Services.AuthService
{
    public class AuthService
    {
        private readonly IBarangayRepository _barangayRepository; // To access barangay data
        private readonly IConfiguration _configuration;

        public AuthService(IBarangayRepository barangayRepository, IConfiguration configuration)
        {
            _barangayRepository = barangayRepository;
            _configuration = configuration;
        }

        // Method to handle user login
        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            // Fetch the Barangay from the database
            var barangay = await _barangayRepository.GetByIdAsync(loginDto.BarangayId); // Assuming GetByIdAsync method

            if (barangay == null)
            {
                throw new UnauthorizedAccessException("Barangay not found");
            }

            // Use bcrypt to verify if the provided password matches the stored hashed password
            bool passwordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, barangay.Password);

            if (!passwordValid)
            {
                throw new UnauthorizedAccessException("Invalid password");
            }

            // Return JWT token if credentials are valid
            var token = GenerateJwtToken(barangay);
            
            return new LoginResponseDTO
            {
                Token = token,
                BarangayId = barangay.BarangayId
            };

        }

        // Method to generate JWT token
        private string GenerateJwtToken(Barangay barangay)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, barangay.BarangayId),
                new Claim(ClaimTypes.Name, barangay.BarangayName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
