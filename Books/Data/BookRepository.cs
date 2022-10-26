using AutoMapper;
using AutoMapper.QueryableExtensions;
using Books.Dto;
using Books.Interfaces;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Data
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly IMapper _mapper;
        public BookRepository(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        

        public async Task<ActionResult<IEnumerable<AllBookDto>>> GetAllBooks()
        {

            return await _context.Books.ProjectTo<AllBookDto>(_mapper.ConfigurationProvider).ToListAsync();

        }
        
        public async Task<Book> GetBooksByIdAsync(int id)
        {
            return await _context.Books.Include(p => p.Images).SingleOrDefaultAsync(i => i.BookId == id);
        }
        public async Task<Book> GetBooksById(int id)
        {
            return await FindByCondition(d => d.BookId.Equals(id)).Include(i => i.Images)
                .FirstOrDefaultAsync();
        }
        public async Task<ActionResult<IEnumerable<GetBookDto>>> GetUnRentedBooks()
        {
            return await _context.Books.ProjectTo<GetBookDto>(_mapper.ConfigurationProvider).Where(i => i.RenterId == 0).ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<GetBookDto>>> GetRentedBooks()
        {
            return await _context.Books.ProjectTo<GetBookDto>(_mapper.ConfigurationProvider).Where(i => i.RenterId != 0).ToListAsync();
        }
        public async Task<IEnumerable<GetBookWithAuthorDto>> GetOwnedBooks(int ownerId)
        {
            return await _context.Books.ProjectTo<GetBookWithAuthorDto>(_mapper.ConfigurationProvider).Where(i => i.OwnerId == ownerId).ToListAsync();
        }
        public async Task<IEnumerable<GetBookWithAuthorDto>> GetUserRentedBooks(int renterId)
        {
            return await _context.Books.ProjectTo<GetBookWithAuthorDto>(_mapper.ConfigurationProvider).Where(i => i.RenterId == renterId).ToListAsync();
        }

        public async Task<Book> GetBookByTitleAsync(string BookTitle)
        {
            return await _context.Books.Where(u => u.BookTitle == BookTitle).SingleOrDefaultAsync();
        }
        public async Task RentBook(RentBookDto rentBook)
        {
            var book = await _context.Books.Where(u => u.BookId == rentBook.BookId).SingleOrDefaultAsync();
            var user = await _context.Users.Include(r => r.RentedBooks).FirstOrDefaultAsync(u => u.Id == rentBook.RenterId);
            book.RenterId = rentBook.RenterId;
            book.Renter = user;
            user.RentedBooks.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task unRentBook(RentBookDto unRentBook)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.BookId == unRentBook.BookId);
            var user = await _context.Users.Include(r => r.RentedBooks).FirstOrDefaultAsync(u => u.Id == unRentBook.RenterId);
            user.RentedBooks.Remove(book);
            book.RenterId = null;
            book.Renter = null;
            await _context.SaveChangesAsync();

        }


        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(u => u.BookId == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(BookUpdateDto book)
        {
            var _book = _context.Books.Where(n => n.BookId == book.BookId).FirstOrDefault();
            _book.BookTitle = book.BookTitle;
            _book.BookYear = book.BookYear;
            _context.SaveChanges();
        }
        public void DeleteBookk(Book book)
        {
            Remove(book);
        }
    
    }
}
