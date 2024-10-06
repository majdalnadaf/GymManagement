using GymManagement.Domains.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);
        Task<Subscription> GetByIdAsync(Guid subscriptionId);

        Task<Subscription> GetByAdminIdAsync(Guid adminId);
        Task<bool> ExistsAsync(Guid subscriptionId);

        Task<List<Subscription>> GetListAsync();
        Task UpdateSubscriptionAsync(Subscription subscription);
        Task RemoveSubscriptionAsync(Subscription subscription);

        Task RemoveRangeAsync(List<Subscription> subscriptions);


    }
}
