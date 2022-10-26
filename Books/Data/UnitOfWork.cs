using AutoMapper;
using Books.Interfaces;
using Books.Models;

namespace Books.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DataContext _context;
        public readonly IMapper _mapper;
        public UnitOfWork(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Authors = new AuthorRepository(_context, _mapper);
            Users = new AppUserRepository(_context, _mapper);
            Books = new BookRepository(_context,_mapper);
            
        }
        public IAuthorRepository Authors { get; private set; }
        public IAppUserRepository Users { get; private set; }
        public IBookRepository Books { get; private set; }
        public IImageService ImageService { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
