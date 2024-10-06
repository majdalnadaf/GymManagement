using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.DeleteGym
{
    public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
    {

        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IGymRepository _gymRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteGymCommandHandler(ISubscriptionRepository subscriptionRepository, IGymRepository gymRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _gymRepository = gymRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand command, CancellationToken cancellationToken)
        {

            var gym = await _gymRepository.GetByIdAsync(command.gymId);
            if (gym is null)
            {
                return Error.NotFound(description: "Gym not found");
            }


            var subscription = await _subscriptionRepository.GetByIdAsync(command.subscriptionId);
            if(subscription is null)
            {
                return Error.NotFound(description: "Subscription not found");
            }

            if(subscription.HasGym(command.gymId) is false)
            {
                return Error.Unexpected(description: "Gym not found");
            }

            subscription.RemoveGym(command.gymId);

            await _subscriptionRepository.UpdateSubscriptionAsync(subscription);
            await _gymRepository.RemoveGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}
