using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using GymManagement.Contract.Subscriptions;
using MediatR;
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using System.Reflection.Metadata.Ecma335;
using GymManagement.Application.Subscriptions.Queries;
using GymManagement.Domains.Subscriptions;
using DomainSubscritpionType = GymManagement.Domains.Subscriptions.SubscriptionType;
using GymManagement.Application.Subscriptions.Queries.GetSubscriptions;

using ContractSubscriptionType = GymManagement.Contract.Subscriptions.SubscriptionType;
using GymManagement.Application.Subscriptions.Commands.DeleteSubscription;

namespace GymManagement.Api.Controllers
{
    [Route("[controller]")]
    public class SubscriptionsController : ApiBaseController
    {

        private readonly IMediator _mediator;

        public SubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
        {
            if (!DomainSubscritpionType.TryFromName(request.SubscriptionType.ToString(), out var subscriptionType))
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid subscription type");
            }


            var command = new CreateSubscriptionCommand(subscriptionType, request.AdminId);

            var createSubscriptionResult = await _mediator.Send(command);


            return createSubscriptionResult.MatchFirst(

                    subscription => CreatedAtAction(
                                    nameof(CreateSubscription),
                                    new { subscriptionId = subscription.Id, },
                                    new SubscriptionResponse(subscription.Id, request.SubscriptionType)),

                    errors => Problem(errors)
                 );


        }


        [HttpGet("{subscriptionId:guid}")]
        public async Task<IActionResult> GetSubscription(Guid subscrptionId)
        {
            var query = new GetSubscriptionQuery(subscrptionId);

            var getSubscriptionResult = await _mediator.Send(query);


            return getSubscriptionResult.MatchFirst(
                subscription => Ok(new SubscriptionResponse(subscription.Id, ToDto(subscription.SubscriptionType))),
                errors => Problem(errors)
                );


        }


        [HttpDelete("{subscriptionId:guid}")]
        public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
        {
            var command = new DeleteSubscriptonCommand(subscriptionId);
            var deleteSubscriptionReuslt = await _mediator.Send(command);

            return deleteSubscriptionReuslt.Match(

                              _ => NoContent(),
                              errors => Problem(errors)
                    );
        }

        private static ContractSubscriptionType ToDto(DomainSubscritpionType subscriptionType)
        {
            return subscriptionType.Name switch
            {
                nameof(DomainSubscritpionType.Free) => ContractSubscriptionType.Free,
                nameof(DomainSubscritpionType.Starter) => ContractSubscriptionType.Starter,
                nameof(DomainSubscritpionType.Pro) => ContractSubscriptionType.Pro,

                _ => throw new InvalidOperationException()
            };
        }

    }
}
