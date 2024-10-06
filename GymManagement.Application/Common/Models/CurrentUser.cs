

using System;
using System.Collections.Generic;

namespace GymMangement.Application.Common.Models;


public record CurrentUser (
  Guid id,
  IReadOnlyList<string> Permissions,
  IReadOnlyList<string> roles

);