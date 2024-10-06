using GymManagement.Application.Authentication.Commads;
using GymManagement.Application.Authentication.Common;
using GymManagement.Contract.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Application.Authentication.Commads.Register;
using GymManagement.Application.Authentication.Querires;
using ErrorOr;



namespace GymManagement.Api.Controllers
{
    [Route("[controller]/")]

    public class AuthenticationController(ISender mediator): ApiBaseController
    {

        private readonly ISender _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register(Contract.Authentication.RegisterRequest  request)
        {
            var command = new RegisterCommand(
                request.firstName,
                request.lastName,
                request.email,
                request.password
                );

            var authResult = await _mediator.Send(command);


           return authResult.Match(

                authResult => Ok(MapToAuthResponse(authResult)),
                errors => Problem(errors)
                
                );



        }

   


      [HttpPost("login")]
      public async Task<IActionResult> Login(LoginRequest request){

        var query = new LoginQuery(
            request.Email,
            request.Password
        );

        var authResult = await _mediator.Send(query);

        return authResult.Match(
            authResult => Ok(MapToAuthResponse(authResult)),
            errors => Problem(errors)
        );

      }



        private AuthenticationResponse MapToAuthResponse(AuthenticationResult authResult)
        {

            return new AuthenticationResponse(
                   id:authResult.user.Id,
                   firstName:authResult.user.FirstNamme,
                   lastName:authResult.user.LastName,
                   email:authResult.user.Email,
                   token:authResult.token

                );

        }

    }
}
