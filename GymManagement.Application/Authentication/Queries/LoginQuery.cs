

using ErrorOr;
using GymManagement.Application.Authentication.Common;
using MediatR;

namespace GymManagement.Application.Authentication.Querires;


public record LoginQuery(
string email,
string password

):IRequest<ErrorOr<AuthenticationResult>>;