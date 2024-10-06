

using FluentValidation;
using GymManagement.Application.Authentication.Querires;

namespace GymManagment.Application.Authentication.Querires;

public class LoginValidator:AbstractValidator<LoginQuery>{


        public LoginValidator()
        {
            RuleFor(x=>x.email).EmailAddress();
            RuleFor(x=>x.password).MinimumLength(10);
        }

}