
using System;
using ErrorOr;
using MediatR;

namespace GymManagement.Application.Profiles.Queries.ListProfiles;

public record ListProfilesQuery(Guid userId)
              :IRequest<ErrorOr<ListProfilesResult>>;