using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Services;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // /api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<UserResponseDTO> usersDTO = await _userService.GetAllUsersAsync();

            return Ok(usersDTO);
        }

        // /api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            UserResponseDTO? user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // /api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            UserResponseDTO user = await _userService.CreateUserAsync(createUserDTO);

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = user.Id },
                user
            );
        }

        // /api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            UserResponseDTO? user = await _userService.UpdateUserAsync(id, updateUserDTO);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // /api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            bool isDeleted = await _userService.DeleteUserAsync(id);
            return isDeleted ? NoContent() : NotFound();
        }
    }
}