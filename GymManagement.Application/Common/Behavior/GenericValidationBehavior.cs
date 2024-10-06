using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Behavior
{
    public class GenericValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
        : IPipelineBehavior<TRequest, TResponse>
          where TRequest : IRequest<TResponse> 
         
    {

        private readonly IValidator<TRequest> _validator =  validator;

        public async  Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {


            if (_validator is null)
            {
                return await next();
            }


            var validationResult = await _validator.ValidateAsync(request);
            if (validationResult.IsValid)
            {
                return await next();
            }


            var errors = validationResult.Errors
                         .ConvertAll(error => Error.Validation(
                         code: error.PropertyName,
                         description: error.ErrorMessage));


            return (dynamic)errors;

        }
    }
}
