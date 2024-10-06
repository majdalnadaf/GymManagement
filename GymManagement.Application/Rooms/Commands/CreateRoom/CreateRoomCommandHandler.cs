using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
    {
        private readonly IGymRepository _gymRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateRoomCommandHandler(IUnitOfWork unitOfWork, IGymRepository gymRepository, ISubscriptionRepository subscriptionRepository)
        {

            _gymRepository = gymRepository;
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Room>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
        {
            var gym = await _gymRepository.GetByIdAsync(command.gymId);
            if (gym is null)
            {
                return Error.NotFound(description: "Gym not found");
            }

            var subscription = await _subscriptionRepository.GetByIdAsync(command.subscriptionId);
            if (subscription is null)
            {
                return Error.NotFound(description: "Subscription not found");
            }

            var room = new Room(
                name: command.roomName,
                gymId: command.gymId,
                maxDailySessions: subscription.GetMaxDailySessions()

                );


            var addRoomResult = gym.AddRoom(room);
            if (addRoomResult.IsError)
            {
                return addRoomResult.Errors;
            }

            await _gymRepository.UpdateGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();


            return room;


        }
    }
}
