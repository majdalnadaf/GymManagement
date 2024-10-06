using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ErrorOr<Deleted>>
    {
        private readonly IGymRepository _gymRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomCommandHandler(IGymRepository gymRepository, IUnitOfWork unitOfWork)
        {
            _gymRepository = gymRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Deleted>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
        {
            var gym = await _gymRepository.GetByIdAsync(command.gymId);
            if(gym is null)
            {
                return Error.NotFound(description: "Gym not found");
            }

            if (gym.HasRoom(command.roomId))
            {
                return Error.Unexpected(description: "Room not found");
            }

            gym.RemoveRoom(command.roomId);
            await _gymRepository.UpdateGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return  Result.Deleted;
        }
    }
}
