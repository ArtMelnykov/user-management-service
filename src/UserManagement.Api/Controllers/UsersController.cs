using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Api.DTOs;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UsersController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<UserResponseDTO> usersDTO = await _applicationDbContext
            .Users
            .Select(user => new UserResponseDTO()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            })
            .ToListAsync();

            return Ok(usersDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            User? user = await _applicationDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserResponseDTO responseDTO = new UserResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            return Ok(responseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = createUserDTO.Email,
                UserName = createUserDTO.UserName,
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                PasswordHash = createUserDTO.Password,
                CreatedAt = DateTime.UtcNow
            };

            _applicationDbContext.Users.Add(user);
            await _applicationDbContext.SaveChangesAsync();

            UserResponseDTO responseDTO = new UserResponseDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            };

            return Created($"/api/users/{user.Id}", responseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            User? user = await _applicationDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Email = updateUserDTO.Email;
            user.UserName = updateUserDTO.UserName;
            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;
            user.UpdatedAt = DateTime.UtcNow;

            await _applicationDbContext.SaveChangesAsync();

            UserResponseDTO responseDTO = new UserResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            return Ok(responseDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            User? user = await _applicationDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _applicationDbContext.Users.Remove(user);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}