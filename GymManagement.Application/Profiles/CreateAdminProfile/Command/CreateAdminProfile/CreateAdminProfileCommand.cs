

using System;
using ErrorOr;
using MediatR;

namespace GymManagement.Application.Profiles.CreateAdminProfile;


public record CreateAdminProfileCommand(Guid userId) : IRequest<ErrorOr<Guid>>;


