using FluentValidation;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommandValidator : AbstractValidator<DeleteSubscriptonCommand>
    {

        public DeleteSubscriptionCommandValidator()
        {
            RuleFor(x => x.subscriptionId).NotEmpty();    

        }
    }
}
