using GymManagement.Application.Rooms.Commands.CreateRoom;
using GymManagement.Application.Rooms.Commands.DeleteRoom;
using GymManagement.Contract.Rooms;
using GymManagement.Domains.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymManagement.Api.Controllers
{
    [Route("gyms/{gymId:guid}/subscriptions/{subscriptionId:guid}/rooms")]
    public class RoomsController : ApiBaseController
    {
        private readonly ISender _mediator;
        public RoomsController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost()]
        public async Task<IActionResult> CreateRoom(CreateRoomRequest request, Guid subscriptionId, Guid gymId)
        {
            var command = new CreateRoomCommand(gymId, subscriptionId, request.name);
            var createRoomResult = await _mediator.Send(command);


            return createRoomResult.Match(
                room => CreatedAtAction(
                    nameof(CreateRoom),
                    new { room.Id, Name = room.Name },
                    new RoomResponse(room.Id, room.Name)),

                errors => Problem(errors)
                );

        }


        [HttpDelete("{roomId:guid}")]
        public async Task<IActionResult> DeleteRoom(Guid gymId, Guid roomId)
        {
            var command = new DeleteRoomCommand(gymId, roomId);

            var deleteRoomResult = await _mediator.Send(command);

            return deleteRoomResult.Match(
                    _ => NoContent(),
                    errors => Problem(errors)

                   );


        }




    }
}
