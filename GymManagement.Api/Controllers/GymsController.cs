using ErrorOr;
using GymManagement.Application.Gyms.Commands.AddTrainer;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Commands.DeleteGym;
using GymManagement.Application.Gyms.Queries.GetGym;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Contract.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymManagement.Api.Controllers
{
    [Route("subscriptions/{subscriptionId:guid}/gyms")]
    public class GymsController : ApiBaseController
    {
        private readonly ISender _mediator;
        public GymsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGym(CreateGymRequest request, Guid subscriptionId)
        {
            var command = new CreateGymCommand(request.name, subscriptionId);

            var createGymResult = await _mediator.Send(command);

            return createGymResult.MatchFirst(

                 gym => CreatedAtAction(
                     nameof(GetGym),
                     new { subscriptionId, GymId = gym.Id },
                     new GymResponse(gym.Id, gym.Name)),

                 errors => Problem(errors)

                 );
        }


        [HttpDelete("{gymId:guid}")]
        public async Task<IActionResult> DeleteGym(Guid subscriptionId, Guid gymId)
        {
            var command = new DeleteGymCommand(subscriptionId, gymId);
            var deleteGymResult = await _mediator.Send(command);

            return deleteGymResult.Match(

                _ => NoContent(),
                errors => Problem(errors)

                );

        }


        [HttpGet()]
        public async Task<IActionResult> ListGyms(Guid subscriptionId)
        {
            var query = new ListGymsQuery(subscriptionId);

            var listGymsResult = await _mediator.Send(query);

            return listGymsResult.Match(

                gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
                errors => Problem(errors)

                );

        }


        [HttpGet("{gymId:guid}")]
        public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId)
        {
            var query = new GetGymQuery(subscriptionId, gymId);

            var getGymResult = await _mediator.Send(query);

            return getGymResult.Match(
                gym => Ok(new GymResponse(gym.Id, gym.Name)),
                errors => Problem(errors)
                );

        }


        [HttpPost("{gymId:guid}/trainers")]
        public async Task<IActionResult> AddTrainer(AddTrainerRequest request, Guid subscriptionId, Guid gymId)
        {
            var command = new AddTrainerCommand(subscriptionId, gymId, request.trainerId);

            var addTrainerResult = await _mediator.Send(command);

            return addTrainerResult.MatchFirst(
                success => Ok(),
                errors => Problem(errors)
                );
        }

    }
}
