


using GymManagement.Application.Profiles.CreateAdminProfile;
using GymManagement.Application.Profiles.Queries.ListProfiles;
using GymManagement.Contract.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace GymManagement.Api.Controllers;




[Route("users/{userId:guid}/profiles")]
public class ProfilesController(IMediator mediator) :ApiBaseController
{

private readonly IMediator _mediator = mediator;

[HttpPost("admin")]
public async Task<IActionResult> CreateAdminProfile(Guid userId)
{
     var command = new CreateAdminProfileCommand(
            userId: userId
     );

    var createResult = await _mediator.Send(command);
     
    return createResult.Match(
       createResult => Ok(userId),
       _=> Problem()
    );

}


[HttpGet()]
public async Task<IActionResult> ListProfiles(Guid userId)
{
   var query = new ListProfilesQuery(userId);

   var ListProfilesResult = await _mediator.Send(query);

   return ListProfilesResult.Match(
           profiles => Ok(new ListProfilesResponse(
                  profiles.adminId,
                  profiles.participantId,
                  profiles.trainerId            
           )),
           _=> Problem()

   ); 
}





}