using AutoMapper;
using Books.Data;
using Books.Dto;
using Books.Interfaces;
using Books.Models;
using Books.Services;
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
    public class BooksController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
       

        public BooksController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BooksController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
         
        }



        //dodavanje knjige
        [HttpPost("UserAddBook")]
        public async Task<ActionResult> AddBook(AddBookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return  BadRequest(ModelState);
            }
            var user = await _unitOfWork.Users.GetUsersByIdAsync(bookDto.OwnerId);
            var book = new Book
            {
                BookTitle = bookDto.BookTitle,
                OwnerId = bookDto.OwnerId,
                Owner = user,
                BookYear = bookDto.BookYear,
               
            };

            _logger.LogInformation("Create Book initiated");
            try
            {
                _unitOfWork.Books.Add(book);
                _unitOfWork.Complete();
                return Ok(bookDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                return BadRequest(ex.Message);
            }
        }

        //prikaz svvih knjiga
        [HttpGet("GetAllBooks")]
        public async Task<ActionResult<IEnumerable<AllBookDto>>> GetAllBooks()
        {
            _logger.LogInformation("GetAllBooks initiated");
            try
            {
                var books = await _unitOfWork.Books.GetAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                return BadRequest(ex.Message);
            }
        }

        //prikaz knjiga po nazivu
        [HttpGet("BookTitle/{BookTitle}")]

        public async Task<IActionResult> GetBookByTitle(string BookTitle)
        {
            try
            {
                var book = await _unitOfWork.Books.GetBookByTitleAsync(BookTitle);
                
                if (book == null)
                {
                    _logger.LogError($"Book with id: {BookTitle}, hasn't been found in db.");
                    return NotFound();
                }
                _logger.LogInformation($"Returned Book for id: {BookTitle}");
                var userResult = _mapper.Map<GetBookDto>(book);
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetBookTitle action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        //dohvacanje knjige po idu
        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {

                var book =  await _unitOfWork.Books.GetBooksById(id);
                if (book == null)
                {
                        _logger.LogError($"Book with id: {id}, hasn't been found in db.");
                        return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned Book for id: {id}");
                    var bookResult = _mapper.Map<BookDto>(book);
                    return Ok(book);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetBookId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //metoda za iznajmljivanje knjige
        [HttpPut("RentBook")]
        public async Task<ActionResult> RentBook(RentBookDto rentBook)
        {

            _logger.LogInformation("RentBook ");
            try
            {
                await _unitOfWork.Books.RentBook(rentBook);
                return Ok("Completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                return BadRequest(ex.Message);
            }
        }

        //metoda za odjavljivanje knjige
        [HttpPut("unRentBook")]
        public async Task<ActionResult> UnRentBook(RentBookDto unRentBook)
        {

            _logger.LogInformation("unRentBook ");
            try
            {
                await _unitOfWork.Books.unRentBook(unRentBook);
                return Ok("Completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                return BadRequest(ex.Message);
            }
        }


        //metoda za prikaz svih knjiga koje nisu rentante
        [HttpGet("unRentedBooks")]
        public async Task<ActionResult<IEnumerable<GetBookDto>>> GetUnRentedBooks()
        {
            _logger.LogInformation("GetUnRentedBooks initiated");
            try
            {
                var books = await _unitOfWork.Books.GetUnRentedBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                return BadRequest(ex.Message);
            }
        }


        //metoda za prikaz svih knjiga koje su iznajmljene
        [HttpGet("RentedBooks")]
        public async Task<ActionResult<IEnumerable<GetBookDto>>> GetRentedBooks()
        {
            _logger.LogInformation("GetRentedBooks initiated");
            try
            {
                var books = await _unitOfWork.Books.GetRentedBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                return BadRequest(ex.Message);
            }
        }
    
        //prikaz svih knjiga od vlasnika
        [HttpGet("OwnerId/{ownerId}")]
        public async Task<ActionResult<IEnumerable<GetBookDto>>> GetOwnedBooks(int ownerId)
        {
            _logger.LogInformation("GeOwnedBooks initiated");
            try
            {
                var books = await _unitOfWork.Books.GetOwnedBooks(ownerId);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("RenterId/{renterId}")]
        public async Task<ActionResult<IEnumerable<GetAppUserDto>>> GetUserRentedBooks(int renterId)
        {
            _logger.LogInformation("GeRentedBooks initiated");
            try
            {
                var books = await _unitOfWork.Books.GetUserRentedBooks(renterId);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            _logger.LogInformation("Delete book initiated");
            try
            {
                await _unitOfWork.Books.DeleteBook(id);
                return Ok("Completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Caught");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-book/{BookTitle}")]
        public async Task<IActionResult> DeleteUser(string BookTitle)
        {
            try
            {
                var book = await _unitOfWork.Books.GetBookByTitleAsync(BookTitle);
                if (book == null)
                {
                    _logger.LogError($"User with id: {BookTitle}, hasn't been found in db.");
                    return NotFound();
                }

                _unitOfWork.Books.DeleteBookk(book);

                _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateBook")]
        public async Task<ActionResult> UpdateBook(BookUpdateDto book)
        {
            _logger.LogInformation("UpdateBook initiated");
            try
            {
                await _unitOfWork.Books.UpdateBook(book);
                return Ok(book);
            }
            
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateBook action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        

    }

}

