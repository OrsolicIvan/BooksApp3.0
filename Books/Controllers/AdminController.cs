using AutoMapper;
using Books.Interfaces;
using Books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StanAPI.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AppUserController> _logger;

        public AdminController(UserManager<AppUser> userManager
           , IUnitOfWork unitOfWork, ILogger<AppUserController> logger)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]

        public async Task<ActionResult> GetUsersWithRoles()
        {
            var customers = await _userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    UserName = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();

            return Ok(customers);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound("Could not find user");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest("Failed to remove from roles");

            return Ok(await _userManager.GetRolesAsync(user));  
        }

        [HttpDelete("delete-id/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUsersByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _unitOfWork.Users.DeleteUser(user);

                _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("delete-user/{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByUsernameAsync(username);
                if (user == null)
                {
                    _logger.LogError($"User with id: {username}, hasn't been found in db.");
                    return NotFound();
                }

                _unitOfWork.Users.DeleteUser(user);

                _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _unitOfWork.Authors.GetAuthorsById(id);
                if (author == null)
                {
                    _logger.LogError($"Author with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _unitOfWork.Authors.DeleteAuthor(author);

                _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteAuthor action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
