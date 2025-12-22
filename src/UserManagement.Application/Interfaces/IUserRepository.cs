using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsersAsync();
        public Task<User?> GetUserByIDAsync(Guid id);
        public Task<User> CreateUserAsync(User user);
        public Task<User> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(Guid id);
    }
}