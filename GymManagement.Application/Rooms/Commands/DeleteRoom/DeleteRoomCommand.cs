using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Rooms.Commands.DeleteRoom
{
    public  record DeleteRoomCommand(
        Guid gymId,
        Guid roomId
        ) : IRequest<ErrorOr<Deleted>>;
    
}
