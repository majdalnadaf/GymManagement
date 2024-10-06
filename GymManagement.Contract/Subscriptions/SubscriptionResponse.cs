using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contract.Subscriptions
{
    public record SubscriptionResponse (Guid SubscriptionId , SubscriptionType SubscriptionType);
    
}
