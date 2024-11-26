﻿using Microsoft.AspNetCore.Mvc;
using LookatBackend.Models;
using LookatBackend.Dtos.CreateRequestRequestDto;
using LookatBackend.Mappers;
using LookatBackend.Dtos.UpdateRequestRequestDto;
using Microsoft.EntityFrameworkCore;

namespace LookatBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly LookatDbContext _context;
        public RequestController(LookatDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var documents = _context.Requests.ToList()
                .Select(r => r.ToRequestDto());

            return Ok(documents);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var document = _context.Requests.Find(id);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateRequestRequestDto requestDto)
        {
            var requestModel = requestDto.ToRequestFromCreateDto();
            _context.Requests.Add(requestModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = requestModel.RequestId }, requestModel.ToRequestDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateRequestRequestDto requestDto)
        {
            var requestModel = _context.Requests.FirstOrDefault(x => x.RequestId == id);

            if (requestModel == null)
            {
                return NotFound();
            }

            requestModel.RequestType = requestDto.RequestType;
            requestModel.DocumentId = requestDto.DocumentId;
            requestModel.Quantity = requestDto.Quantity;

            _context.SaveChanges();

            return Ok(requestModel.ToRequestDto());
        }
    }
}
