using Books.Dto;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        new IEnumerable<Book> GetAll();
        Task<Book> GetBooksById(int id);
        Task<Book> GetBooksByIdAsync(int id);
        Task<ActionResult<IEnumerable<GetBookDto>>> GetUnRentedBooks();
        Task<ActionResult<IEnumerable<GetBookDto>>> GetRentedBooks();
        Task<ActionResult<IEnumerable<AllBookDto>>> GetAllBooks();
        Task<IEnumerable<GetBookWithAuthorDto>> GetOwnedBooks(int id);
        Task<IEnumerable<GetBookWithAuthorDto>> GetUserRentedBooks(int id);
        Task<Book> GetBookByTitleAsync(string BookTitle);
        Task unRentBook(RentBookDto rentBook);
        Task RentBook(RentBookDto rentBook);
        Task DeleteBook(int id);
        void DeleteBookk(Book book);
        Task UpdateBook(BookUpdateDto book);
    }
}
