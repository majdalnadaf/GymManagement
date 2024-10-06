

using GymManagement.Application.Authentication.Common.interfaces;
using GymMangement.Application.Common.Models;

namespace GymManagement.Api.Authentication;

public class CurrentUserProvider(IHttpContextAccessor httpContextAccessor) 
: ICurrentUserProvider

{

    private  readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public CurrentUser GetCurrentuser()
    {
        if(_httpContextAccessor is null)
          throw new Exception();

         var id = GetClaimsValues("id")
                  .Select(Guid.Parse) 
                  .First();

        var permissions = GetClaimsValues("permissions");
        var roles = GetClaimsValues("roles");
       
        return new CurrentUser(id,permissions,roles);

    }

    private IReadOnlyList<string> GetClaimsValues(string claimType)
    {

            return _httpContextAccessor.HttpContext!.User.Claims
           .Where(claim => claim.Type == claimType)
           .Select(claim => claim.Value)
           .ToList();
 
    }
}
