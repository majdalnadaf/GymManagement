using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Queries.ListGyms
{
    public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
    {

        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IGymRepository _gymRepository;


        public ListGymsQueryHandler(IGymRepository gymRepository, ISubscriptionRepository subscriptionRepository)
        {
            _gymRepository = gymRepository;
            _subscriptionRepository = subscriptionRepository;
        }
        public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery query, CancellationToken cancellationToken)
        {

            if (await _subscriptionRepository.ExistsAsync(query.subscriptionId))
            {
                return Error.NotFound(description: "Subscription not found");
            }


            var gyms = await _gymRepository.ListBySubscriptionIdAsync(query.subscriptionId);
            if(gyms is null)
            {
                return new List<Gym>();
            }


            return gyms;

        }
    }
}
