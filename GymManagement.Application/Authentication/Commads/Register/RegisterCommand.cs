using ErrorOr;
using GymManagement.Application.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Authentication.Commads.Register
{
    public record RegisterCommand(
        string firstName,
        string lastName,
        string email,
        string password

        ):IRequest<ErrorOr<AuthenticationResult>>;
    
}
