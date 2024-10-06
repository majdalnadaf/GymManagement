using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.CreateGym
{
    public class CreateGymCommandValidator :AbstractValidator<CreateGymCommand>
    {
        public CreateGymCommandValidator()
        {
            RuleFor(x => x.name).MaximumLength(200);
            RuleFor(x => x.subscriptionId).NotEmpty();
        }

    }
}
