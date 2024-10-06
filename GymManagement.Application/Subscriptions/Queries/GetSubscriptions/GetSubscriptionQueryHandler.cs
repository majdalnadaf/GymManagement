using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Queries.GetSubscriptions
{
    public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;


        public GetSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }
        public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery query, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(query.SubscriptionId);

            return subscription is null
                ? Error.NotFound(description: "Subscription Not Found")
                : subscription;

        }
    }
}
