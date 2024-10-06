using GymManagement.Domains.Admins.Events;
using GymManagement.Domains.Common;
using GymManagement.Domains.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domains.Admins
{
    public class Admin : Entity
    {

        public Guid UserId { get; }
        public Guid? SubscriptionId { get; private set; } = null!;

        public Admin(
            Guid userId,
            Guid? subscriptionId = null,
            Guid? id = null) :base(id ?? Guid.NewGuid())
        {
            UserId = userId;
            SubscriptionId = subscriptionId;
        }

        private Admin()
        {

        }


        public void SetSubscription(Subscription subscriptions)
        {
            if (SubscriptionId is not null)
            {

                // logic 

            }

            SubscriptionId = subscriptions.Id;
        }

        public void DeleteSubscription(Guid subscriptionId)
        {

            SubscriptionId = null!;

            _domainEvents.Add(new DeleteSubscriptionEvent(subscriptionId));
        }

    }
}
