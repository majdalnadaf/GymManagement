using ErrorOr;
using GymManagement.Domains.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(
        Guid gymId,
        Guid subscriptionId,
        string roomName

        ) : IRequest<ErrorOr<Room>>;

}
