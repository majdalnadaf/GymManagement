

namespace GymManagement.Contract.Profiles;


public record ListProfilesResponse(
    Guid? adminId,
    Guid? participantId,
    Guid? trainerId
);