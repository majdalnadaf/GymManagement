using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.AddTrainer
{
    public class AddTrainerCommandValidator : AbstractValidator<AddTrainerCommand>
    {

        public AddTrainerCommandValidator()
        {
            RuleFor(p => p.trainerId).NotEmpty();
            RuleFor(p => p.subscriptionId).NotEmpty();
            RuleFor(p => p.gymId).NotEmpty();
                
        }
    }
}
