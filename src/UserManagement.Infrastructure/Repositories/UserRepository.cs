using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _applicationDbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIDAsync(Guid id)
        {
            return await _applicationDbContext.Users.FindAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _applicationDbContext.Users.Add(user);

            await _applicationDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _applicationDbContext.Users.Update(user);

            await _applicationDbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            User? user = await _applicationDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            _applicationDbContext.Users.Remove(user);

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}