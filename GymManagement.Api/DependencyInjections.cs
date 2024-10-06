

using GymManagement.Api.Authentication;
using GymManagement.Application;
using GymManagement.Application.Authentication.Common.interfaces;
using GymManagement.Infrastructure;
using Microsoft.OpenApi.Models;

namespace GymManagement.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPeresentaion(this IServiceCollection services)
    {
        

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddProblemDetails();
        services.AddSwaggerGen(sg =>
        {


            sg.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            sg.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

        });
   
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        return services;

    }
}