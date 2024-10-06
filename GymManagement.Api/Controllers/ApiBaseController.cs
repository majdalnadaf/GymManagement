
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;


namespace GymManagement.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {


        [HttpGet("/Problem")]
        
        public IActionResult Problem(List<Error> errors)
        {
            if(errors.Count is 0)
            {
                return Problem();
            }

            if(errors.All(error=> error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            return Problem(errors[0]);
        }


        [HttpGet()]
        public IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Unexpected => StatusCodes.Status400BadRequest,

                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, detail: error.Description);

        }

        [HttpGet("/ValidationProblem")]
        public IActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDicunary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDicunary.AddModelError(
                    error.Code,
                    error.Description                   
                    );
            }

            return ValidationProblem(modelStateDicunary);

        }


    }



}
