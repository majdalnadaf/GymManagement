using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Gyms.Persistence
{
    internal class GymRepository : IGymRepository
    {
        private readonly GymManagementDbContext _context;

        public GymRepository(GymManagementDbContext context)
        {
            _context = context;
        }

        public Task AddGymAsync(Gym gym)
        {
            _context.Add(gym);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Gyms.AsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task<Gym> GetByIdAsync(Guid id)
        {
            return await _context.Gyms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
        {
            return _context.Gyms.Where(gym =>
                             gym.SubscriptionId == subscriptionId)
                            .ToListAsync();
        }

        public Task RemoveGymAsync(Gym gym)
        {
            _context.Gyms.Remove(gym);
            return Task.CompletedTask;

        }

        public Task RemoveRangeAsync(List<Gym> gyms)
        {
            _context.Gyms.RemoveRange(gyms);
            return Task.CompletedTask;
        }
        public Task UpdateGymAsync(Gym gym)
        {
            _context.Gyms.Update(gym);
            return Task.CompletedTask;
        }
    }
}
