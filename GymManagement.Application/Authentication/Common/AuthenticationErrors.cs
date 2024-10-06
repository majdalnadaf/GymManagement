using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Authentication.Common
{
    public static class AuthenticationErrors
    {

        public static Error InvalidCreadentials = Error.Validation(
            code: "Authentication.InvalidCreadentials",
            description: "Invalid Creadentials"
            );

    }
}
