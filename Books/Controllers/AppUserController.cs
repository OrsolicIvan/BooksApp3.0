using AutoMapper;
using AutoMapper.QueryableExtensions;
using Books.Dto;
using Books.Interfaces;
using Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StanAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    [Authorize]
    public class AppUserController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AppUserController> _logger;
        private readonly IImageService _imageService;
        private readonly IBookRepository _bookRepository;
        private readonly DataContext _context;

        public AppUserController(IMapper mapper,
            IUnitOfWork unitOfWork, ILogger<AppUserController> logger, IImageService imageService, IBookRepository bookRepository, DataContext context)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _imageService = imageService;
            _bookRepository = bookRepository;
            _context = context;

        }


        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<GetAppUserDto>>> GetAllUsers()
        {
            try
            {
                var books = await _unitOfWork.Users.GetAllUsers();
                _logger.LogInformation($"Returned all users from database");
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {

                var user = await _unitOfWork.Users.GetUsersByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned User for id: {id}");
                    var userResult = _mapper.Map<GetAppUserDto>(user);
                    return Ok(userResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUsersId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("Username/{username}")]

        public async Task<IActionResult> GetUserByUsername(string username)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByUsernameAsync(username);

                if (user == null)
                {
                    _logger.LogError($"User with id: {username}, hasn't been found in db.");
                    return NotFound();
                }
                _logger.LogInformation($"Returned User for id: {username}");
                var userResult = _mapper.Map<GetAppUserDto>(user);
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUsersId action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [HttpPost("add-image")]
        public async Task<ActionResult<List<ImageDto>>> AddPhoto(int id, IFormFile file)
        {
            try
            {
                var book = await _unitOfWork.Books.GetBooksByIdAsync(id);
                var result = await _imageService.AddPhotoAsync(file);

                if (result.Error != null) return BadRequest(result.Error.Message);
                var image = new Image
                {
                
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId
                };

                _context.Images.Add(image);
                book.Images = image;
                await _context.SaveChangesAsync();
                return Ok("Success");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddPhoto action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }





        }
    }
}
