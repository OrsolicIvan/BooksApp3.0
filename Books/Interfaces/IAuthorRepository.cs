using Books.Dto;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<ActionResult<IEnumerable<GetAuthorDto>>> GetAllAuthors();
        Task<Author> GetAuthorsById(int id);
        Task<AddAuthorDto> CreateBookAuthor(AddAuthorDto authorDT);
        GetAuthorBooksDto GetAuthorsWithBooks(int authorId);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
    }
}
