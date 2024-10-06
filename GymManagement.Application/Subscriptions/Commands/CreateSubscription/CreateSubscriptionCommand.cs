using ErrorOr;
using GymManagement.Domains.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommand : IRequest<ErrorOr<Subscription>>
    {

        public Guid AdminId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public CreateSubscriptionCommand(SubscriptionType subscriptionType, Guid adminId)
        {
            AdminId = adminId;
            SubscriptionType = subscriptionType;
        }
    }
}
