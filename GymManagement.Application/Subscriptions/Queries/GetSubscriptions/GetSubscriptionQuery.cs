using ErrorOr;
using GymManagement.Domains.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Queries.GetSubscriptions
{
    public class GetSubscriptionQuery : IRequest<ErrorOr<Subscription>>
    {

        public Guid SubscriptionId { get; set; }

        public GetSubscriptionQuery(Guid subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }

    }
}
