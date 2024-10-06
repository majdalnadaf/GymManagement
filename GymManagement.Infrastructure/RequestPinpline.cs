using GymManagement.Infrastructure.Common.Middlware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure
{
    public static class RequestPinpline
    {
        public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<EventualConsistancyMiddleware>();
            return builder;
        }
    }
}
