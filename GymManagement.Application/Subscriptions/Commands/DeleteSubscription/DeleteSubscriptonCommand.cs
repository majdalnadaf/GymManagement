using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription
{
    public record DeleteSubscriptonCommand(Guid subscriptionId) :IRequest<ErrorOr<Deleted>>;

}
