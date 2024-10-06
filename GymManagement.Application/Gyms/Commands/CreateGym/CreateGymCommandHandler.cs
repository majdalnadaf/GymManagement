using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Gyms;
using GymManagement.Application.common.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.CreateGym
{
    public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
    {


        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IGymRepository _gymRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateGymCommandHandler(ISubscriptionRepository subscriptionRepository, IGymRepository gymRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _gymRepository = gymRepository;
            _unitOfWork = unitOfWork;   

        }

        [Authorize(Permissions = "gyms:create")]
        public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
        {

            var subscription = await _subscriptionRepository.GetByIdAsync(command.subscriptionId);

            if (subscription is null)
            {
                return Error.NotFound(description: "Subscription not found");
            }

            var gym = new Gym(

                name: command.name,
                maxRoom: subscription.GetMaxRooms(),
                subscriptionId: subscription.Id
                );

            var addGymResult = subscription.AddGym(gym);

            if (addGymResult.IsError)
            {
                return addGymResult.Errors;
            }

            await _subscriptionRepository.UpdateSubscriptionAsync(subscription);
            await _gymRepository.AddGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return gym;


        }
    }
}
