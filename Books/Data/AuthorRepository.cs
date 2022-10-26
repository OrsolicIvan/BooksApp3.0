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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly IMapper _mapper;
        public AuthorRepository(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<GetAuthorDto>>> GetAllAuthors()
        {

            return await _context.Author.ProjectTo<GetAuthorDto>(_mapper.ConfigurationProvider).ToListAsync();

        }

        public async Task<Author> GetAuthorsById(int id)
        {
            return await FindByCondition(d => d.AuthorId.Equals(id))
                .FirstOrDefaultAsync();
        }
        public GetAuthorBooksDto GetAuthorsWithBooks(int authorId)
        {
            var _author = _context.Author.Where(n => n.AuthorId == authorId).Select(n => new GetAuthorBooksDto()
            {
                AuthorName = n.AuthorName,
                BookTitles = n.Book_Authors.Select(n => n.Book.BookTitle).ToList()
            }).FirstOrDefault();

            return _author;
        }

        public void DeleteAuthor(Author author)
        {
            Remove(author);
        }
        public void UpdateAuthor(Author author)
        {
            Update(author);
        }

        public async Task<AddAuthorDto> CreateBookAuthor(AddAuthorDto authorDT)
        {
            var author = await _context.Author.Where(i => i.AuthorName == authorDT.AuthorName).SingleOrDefaultAsync();
            var book = await _context.Books.Where(i => i.BookId == authorDT.BookId).FirstOrDefaultAsync();
            if (author != null)
            {
                var book1 = _context.Books.Where(i => i.BookId == authorDT.BookId).SingleOrDefaultAsync();
                    var bookAuthorr = new Book_Author
                {
                    BookId = book.BookId,
                    Book = book,
                    AuthorId = author.AuthorId,
                    Author = author
                };
                
                _context.Books_Authors.Add(bookAuthorr);
                author.Book_Authors.Add(bookAuthorr);
                book.Book_Authors.Add(bookAuthorr);
                await _context.SaveChangesAsync();
                return (authorDT);
            }
            else
            {
                author = new Author
                {
                    AuthorName = authorDT.AuthorName,
                };
                _context.Author.Add(author);
                await _context.SaveChangesAsync();
                author = await _context.Author.Where(i => i.AuthorName == authorDT.AuthorName).SingleOrDefaultAsync();
                var bookAuthor = new Book_Author
                {
                    BookId = book.BookId,
                    Book = book,
                    AuthorId = author.AuthorId,
                    Author = author
                };
                _context.Books_Authors.Add(bookAuthor);
                author.Book_Authors.Add(bookAuthor);
                book.Book_Authors.Add(bookAuthor);
                await _context.SaveChangesAsync();
                return (authorDT);
            }
        }
    }
}
