using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Users;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Users.Persistence
{
    internal class UserRepository(GymManagementDbContext context): IUserRepository
    {

        private readonly GymManagementDbContext _context = context;
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user); 
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FindAsync(email);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public  Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);

            return Task.CompletedTask;
        
        }
    }
}
