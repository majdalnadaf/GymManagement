
using System;

namespace GymManagement.Application.Profiles.Queries.ListProfiles;


public record ListProfilesResult(
    Guid? adminId,
    Guid? participantId,
    Guid? trainerId
    
     );