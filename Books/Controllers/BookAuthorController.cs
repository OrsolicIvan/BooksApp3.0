using AutoMapper;
using Books.Dto;
using Books.Interfaces;
using Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StanAPI.Controllers;
using System.Threading.Tasks;

namespace Books.Controllers
{
    [Authorize]
    public class BookAuthorController : BaseApiController
    {

        private readonly IUnitOfWork _unitOfWork;

      
       

        public BookAuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          
       
         
        }

        //dodavanje autora na knjigu
        [HttpPost("AddBookAuthor")]
        public async Task<ActionResult> AddCreateAuthor(AddAuthorDto authorDT)
        {
            await _unitOfWork.Authors.CreateBookAuthor(authorDT);
            return Ok();
        }
    }
}
