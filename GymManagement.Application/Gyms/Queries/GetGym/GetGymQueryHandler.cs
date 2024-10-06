using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymManagement.Application.Gyms.Queries.GetGym
{
    public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IGymRepository _gymRepository;
        

        public GetGymQueryHandler(IGymRepository gymRepository, ISubscriptionRepository subscriptionRepository)
        {
            _gymRepository = gymRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<ErrorOr<Gym>> Handle(GetGymQuery query, CancellationToken cancellationToken)
        {

            if(await _subscriptionRepository.ExistsAsync(query.subscriptionId))
            {
                return Error.NotFound(description: "Subscription not found");
            }

            if(await _gymRepository.GetByIdAsync(query.gymId) is not Gym gym)
            {
                return Error.NotFound(description : "Gym not found");
            }

            return gym;
            
        }

    }
}
