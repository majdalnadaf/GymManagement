

using System;
using System.Threading;
using ErrorOr;
using GymManagement.Application.Authentication.Common.interfaces;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Admins;
using MediatR;

namespace GymManagement.Application.Profiles.CreateAdminProfile;

public class CreateAdminProfileCommandHandler(
    IAdminRepository adminRepository,
    ICurrentUserProvider currentUserProvider,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    )

   : IRequestHandler<CreateAdminProfileCommand, ErrorOr<Guid>>
{

     private readonly IAdminRepository _adminRepository = adminRepository;
     private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;
     private readonly IUserRepository _userRepository = userRepository;
     private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async System.Threading.Tasks.Task<ErrorOr<Guid>> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
    {
            var currentUser =  _currentUserProvider.GetCurrentuser();        
            if(currentUser.id != command.userId)
            {
                return Error.Unauthorized(description:"User is forbidden from taking this action");
            }

            var user = await _userRepository.GetByIdAsync(command.userId);
            if(user is null)
            {
                return Error.NotFound(description:"User not found");
            }

           var createAdminProfileResult =  user.CreateAdminProfile();
           if(createAdminProfileResult.IsError)
           {
             return createAdminProfileResult.Errors;
           }
   
           
           var admin = new Admin(userId: user.Id, id:createAdminProfileResult.Value);

           await _userRepository.UpdateUserAsync(user);
           await _adminRepository.AddAdminAsync(admin);
           
           await _unitOfWork.CommitChangesAsync();

           return createAdminProfileResult.Value;


    }

}