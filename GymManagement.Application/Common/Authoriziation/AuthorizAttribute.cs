

using System;
using System.Collections.Generic;

namespace GymManagement.Application.common.Authorization;


public class AuthorizeAttribute : Attribute
{
    public string? Permissions {get; set;}
    public string? Roles {get;set;}
}