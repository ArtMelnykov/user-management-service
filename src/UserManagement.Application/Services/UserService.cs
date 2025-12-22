using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            List<User> users = await _userRepository.GetAllUsersAsync();

            return users.Select(user => new UserResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            })
            .ToList();
        }

        public async Task<UserResponseDTO?> GetUserByIdAsync(Guid id)
        {
            User? user = await _userRepository.GetUserByIDAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserResponseDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password);

            User user = new User
            {
                Email = createUserDTO.Email,
                UserName = createUserDTO.UserName,
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                PasswordHash = hashPassword,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            User createdUser = await _userRepository.CreateUserAsync(user);

            return new UserResponseDTO
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                UserName = createdUser.UserName,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                IsActive = createdUser.IsActive,
                CreatedAt = createdUser.CreatedAt
            };
        }

        public async Task<UserResponseDTO?> UpdateUserAsync(Guid id, UpdateUserDTO updateUserDTO)
        {
            User? user = await _userRepository.GetUserByIDAsync(id);

            if (user == null)
            {
                return null;
            }

            user.Email = updateUserDTO.Email;
            user.UserName = updateUserDTO.UserName;
            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;

            if (!string.IsNullOrEmpty(updateUserDTO.Password))
            {
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(updateUserDTO.Password);

                user.PasswordHash = hashPassword;
            }

            User updatedUser = await _userRepository.UpdateUserAsync(user);

            return new UserResponseDTO
            {
                Id = updatedUser.Id,
                Email = updatedUser.Email,
                UserName = updatedUser.UserName,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                IsActive = updatedUser.IsActive,
                CreatedAt = updatedUser.CreatedAt
            };
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}