using Books.Dto;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Interfaces
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        new Task<ActionResult<IEnumerable<GetAppUserDto>>> GetAllUsers();
        new void Update(AppUser user);
        Task AddPhoto(Image image, int id);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUsersByIdAsync(int id);
        Task<GetAppUserDto> GetUserByIdAsync2(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        void DeleteUser(AppUser user);
        
    }
}
