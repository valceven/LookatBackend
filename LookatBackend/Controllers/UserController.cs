using Microsoft.AspNetCore.Mvc;
using LookatBackend.Models;
using LookatBackend.Mappers;
using LookatBackend.Dtos.User;
using LookatBackend.Dtos.UpdateUser;
using Microsoft.EntityFrameworkCore;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LookatDbContext _context;

        public UserController(LookatDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users
                .Select(s => s.ToUserDto())
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToUserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            var userModel = userDto.ToUserFromCreateDto();
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = userModel.UserId }, userModel.ToUserDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto updateDto)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (userModel == null)
            {
                return NotFound();
            }

            userModel.UserName = updateDto.UserName;
            userModel.FirstName = updateDto.FirstName;
            userModel.LastName = updateDto.LastName;
            userModel.Password = updateDto.Password;
            userModel.MobileNumber = updateDto.MobileNumber;
            userModel.Date = updateDto.Date;
            userModel.PhysicalIdNumber = updateDto.PhysicalIdNumber;
            userModel.Purok = updateDto.Purok;
            userModel.BarangayLoc = updateDto.BarangayLoc;
            userModel.CityMunicipality = updateDto.CityMunicipality;
            userModel.Province = updateDto.Province;
            userModel.Email = updateDto.Email;

            await _context.SaveChangesAsync();

            return Ok(userModel.ToUserDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (userModel == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
