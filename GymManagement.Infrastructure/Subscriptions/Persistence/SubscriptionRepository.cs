using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Subscriptions.Persistence
{
    internal class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly GymManagementDbContext _context;

        public SubscriptionRepository(GymManagementDbContext context)
        {
            _context = context;

        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid subscriptionId)
        {
            return await
               _context.Subscriptions
               .AsNoTracking()
               .AnyAsync(s => s.Id == subscriptionId);
        }

        public async Task<Subscription> GetByAdminIdAsync(Guid adminId)
        {
            return await _context.Subscriptions
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s._adminId == adminId);

        }

        public async Task<Subscription> GetByIdAsync(Guid subscriptionId)
        {
            //return await _context.Subscriptions
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(s => s.Id == subscriptionId);


            return await _context.Subscriptions.FindAsync(subscriptionId);
        }

        public async Task<List<Subscription>> GetListAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> GetSubscriptionAsync(Guid subscriptionId)
        {
            var subscription = _context.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
            return await Task.FromResult(subscription);
        }

        public Task RemoveRangeAsync(List<Subscription> subscriptions)
        {
            _context.Subscriptions.RemoveRange(subscriptions);
            return Task.CompletedTask;
        }

        public Task RemoveSubscriptionAsync(Subscription subscription)
        {
            _context.Subscriptions.Remove(subscription);
            return Task.CompletedTask;  
        }

        public Task UpdateSubscriptionAsync(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            return Task.CompletedTask;  
        }
    }
}
