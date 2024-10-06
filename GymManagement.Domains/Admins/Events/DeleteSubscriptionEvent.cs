using GymManagement.Domains.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domains.Admins.Events
{
    public record DeleteSubscriptionEvent(Guid subscriptionId) : IDomainEvents;
    
}
