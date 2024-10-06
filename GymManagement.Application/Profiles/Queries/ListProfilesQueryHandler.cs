

using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Profiles.Queries.ListProfiles;

public class ListProfilesQueryHandler(IUserRepository userRepository)                        
 : IRequestHandler<ListProfilesQuery, ErrorOr<ListProfilesResult>>

{

   private readonly IUserRepository _userRepository = userRepository; 

    public async Task<ErrorOr<ListProfilesResult>> Handle(ListProfilesQuery query, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByIdAsync(query.userId);
    if(user is null)
    {
        return Error.NotFound(description: "User not found" );
    }

    return new ListProfilesResult(user.AdminId,user.ParticipantId,user.TrainerId);
    
    }
}
