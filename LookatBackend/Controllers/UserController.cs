using System;
using Microsoft.AspNetCore.Mvc;
using LookatBackend.Models;
using LookatBackend.Mappers;
using LookatBackend.Dtos;

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
        public IActionResult GetAll()
        {
            var users = _context.Users.ToList()
             .Select(s => s.ToUserDto()); // this is to select only the data to be given making it more secure because for example
            // we dont want to return a password right?

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserRequestDto userDto)
        {
            var userModel = userDto.ToUserFromCreateDto();
            _context.Users.Add(userModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = userModel.UserId}, userModel.ToUserDto());
        }

    }
}
