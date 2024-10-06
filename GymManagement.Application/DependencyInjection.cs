
using FluentValidation;
using FluentValidation.AspNetCore;
using GymManagement.Application.Common.Behavior;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymManagement.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {


            services.AddMediatR(options =>
            {
                // Find  classes that implement IRequest interface and register them 
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
                options.AddOpenBehavior(typeof(GenericValidationBehavior<,>));

            });

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection)); // From " FluentValidation.AspNetCor " 

            return services;

        }


  
    }
}
