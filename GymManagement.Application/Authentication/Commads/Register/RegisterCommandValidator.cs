using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Authentication.Commads.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {

        public RegisterCommandValidator()
        {
            RuleFor(x => x.firstName).MaximumLength(200);
            RuleFor(x => x.lastName).MaximumLength(200);
            RuleFor(x => x.email).MaximumLength(500).EmailAddress();
            RuleFor(x => x.password).MinimumLength(10);
            
        }
    }
}
