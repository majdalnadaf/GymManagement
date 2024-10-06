

using System.Runtime;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using GymManagement.Application.Authentication.Common.interfaces;
using MediatR;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using GymManagement.Domains.Gyms;

namespace GymManagement.Application.common.Behavior;


public class AuthroiziationBehavior<TRequest, TResponse>(ICurrentUserProvider currentUserProvider)
         : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
         where TResponse : IErrorOr
{


   private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;

    [AllowAnonymous()]
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
         

         var authorizationAttributes = request.GetType()
         .GetCustomAttributes<GymManagement.Application.common.Authorization.AuthorizeAttribute>()
         .ToList();
       
         if(authorizationAttributes.Count is 0)
         {
              return  await next();
         }


         var currentUser = _currentUserProvider.GetCurrentuser();

         var requiredPermissions = authorizationAttributes
             .SelectMany(authorizationAttribute => authorizationAttribute.Permissions.Split(",") ?? [])
             .ToList();

        if(requiredPermissions.Except(currentUser.Permissions).Any())
        {
           return (dynamic) Error.Unauthorized(description:"User is forbidden from taking this action");
        }

        
        return await next();

         


    }
}