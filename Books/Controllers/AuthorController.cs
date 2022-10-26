using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Books.Models;
using Books.Dto;
using AutoMapper;
using Serilog;
using StanAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Books.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;

namespace Books.Controllers
{
    [Authorize]
    public class AuthorController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorController> _logger;


        public AuthorController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AuthorController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        //dodavanje autora
        [HttpPost("add-author")]

        public async Task<ActionResult> AddAuthor(AddAuthorDto addAuthorDto)
        {
            try
            {
                if (addAuthorDto == null)
                {
                    _logger.LogError("Author object sent from client is null.");
                    return BadRequest("Author object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid author object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var author = new Author
                {
                    AuthorName = addAuthorDto.AuthorName,
                  
                };

                _unitOfWork.Authors.Add(author);
                _unitOfWork.Complete();

                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddAuthor action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Author

        [HttpGet("GetAllAuthors")]
        public async Task<ActionResult<IEnumerable<GetAuthorDto>>> GetAllAuthors()
        {
            try
            {
                var AuthorInfo = await _unitOfWork.Authors.GetAllAuthors();
                _logger.LogInformation($"Returned all authors from database");
                return Ok(AuthorInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}", Name = "AuthorById")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {

                var author = await _unitOfWork.Authors.GetAuthorsById(id);
                if (author == null)
                {
                    _logger.LogError($"Author with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned Author for id: {id}");
                    var authorResult = _mapper.Map<AuthorDto>(author);
                    return Ok(authorResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAuthorId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorUpdateDto author)
        {
            try
            {
                if (author == null)
                {
                    _logger.LogError("Author object sent from client is null.");
                    return BadRequest("Author object is null");
                }

                var authorEntity = await _unitOfWork.Authors.GetAuthorsById(id);
                if (authorEntity == null)
                {
                    _logger.LogError($"Author with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(author, authorEntity);
                _unitOfWork.Authors.UpdateAuthor(authorEntity);
                _unitOfWork.Complete();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateAuthor action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        
    }
}
