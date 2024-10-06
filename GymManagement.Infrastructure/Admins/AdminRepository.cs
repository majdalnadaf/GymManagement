using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Admins;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Admins
{
    internal class AdminRepository : IAdminRepository
    {

        private readonly GymManagementDbContext _context;
        public AdminRepository(GymManagementDbContext context)
        {
            _context = context;
        }

        public Task AddAdminAsync(Admin admin)
        {
            
            _context.Admins.Add(admin);
            return Task.CompletedTask;
        }

        public async Task<Admin> GetByIdAsync(Guid adminId)
        {
           return  await _context.Admins.FirstOrDefaultAsync(a => a.Id == adminId);

        }

        public Task UpdateAdminAsync(Admin admin)
        {
             _context.Admins.Update(admin);
            return Task.CompletedTask;
        }
    }
}
