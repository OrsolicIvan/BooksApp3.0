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
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        private new readonly DataContext _context;
        private readonly IMapper _mapper;
        public AppUserRepository(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public new async Task<ActionResult<IEnumerable<GetAppUserDto>>> GetAllUsers()
        {

            return await _context.Users.ProjectTo<GetAppUserDto>(_mapper.ConfigurationProvider).ToListAsync();

        }
        public async Task<AppUser> GetUsersByIdAsync(int id)
        {
            return await _context.Users.Where(I=>I.Id == id).SingleOrDefaultAsync();
        }

        public async Task<GetAppUserDto> GetUserByIdAsync2(int Id)
        {
            return await _context.Users
                .ProjectTo<GetAppUserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(i => i.Id == Id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Where(u => u.UserName == username).SingleOrDefaultAsync();  
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task AddPhoto(Image image, int id)
        {
            var book = await _context.Books.Include(p => p.Images).SingleOrDefaultAsync(i => i.BookId == id);
            book.Images = image;
            await _context.SaveChangesAsync();

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public new void Update(AppUser customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }
        public void DeleteUser(AppUser user)
        {
            Remove(user);
        }
    }
}
