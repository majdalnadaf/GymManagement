

using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using GymManagement.Application.Authentication.Common;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Common.Interfaces;
using MediatR;


namespace GymManagement.Application.Authentication.Querires;

public class LoginQueryHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator
    )
 : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
 
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
       

        var user = await _userRepository.GetByEmailAsync(query.email);
        
        return user is null || !user.IsCorrectPasswordHash(query.password,_passwordHasher)
         ? AuthenticationErrors.InvalidCreadentials
        : new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));


    }
}