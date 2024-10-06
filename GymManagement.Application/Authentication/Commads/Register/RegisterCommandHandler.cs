using ErrorOr;
using GymManagement.Application.Authentication.Common;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Common.Interfaces;
using GymManagement.Domains.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Authentication.Commads.Register
{
    public class RegisterCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator
        )

        : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;  
        private readonly IJwtTokenGenerator _jwtTokenGenerator1 = jwtTokenGenerator;

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            if (await _userRepository.ExistsByEmailAsync(request.email) is true)
            {
                return Error.Conflict(description: "Email is already exists");
            }

            var hashResult = _passwordHasher.HashPassword(request.password);
            if (hashResult.IsError)
            {
                return hashResult.Errors;
            }


            var user = new User(
                firstName: request.firstName,
                lastName: request.lastName,
                email: request.email,
                passwordHash: hashResult.Value
                );


            await _userRepository.AddUserAsync(user);
            await _unitOfWork.CommitChangesAsync();

            return new AuthenticationResult(
                user: user,
                token: _jwtTokenGenerator1.GenerateToken(user)
                );




        }
    }
}
