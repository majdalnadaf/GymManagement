using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Admins;
using GymManagement.Infrastructure.Authentication.TokenGenerator;
using GymManagement.Infrastructure.Common.Persistence;
using GymManagement.Infrastructure.Gyms.Persistence;
using GymManagement.Infrastructure.Subscriptions.Persistence;
using GymManagement.Infrastructure.Users.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Options;
using GymManagement.Domains.Common.Interfaces;
using GymManagement.Infrastructure.Authentication.PasswrodHasher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace GymManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrasturcture(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<GymManagementDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"))
            );
            
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GymManagementDbContext>());
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IGymRepository, GymRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddHttpContextAccessor();

            services.AddAuthentication(configuration);
            return services;
        }


        public static IServiceCollection AddAuthentication(this IServiceCollection services , IConfiguration configuration)
        {

              var jwtSettings = new JwtSettings();
              var jwtSection = configuration.GetSection("JwtSettings");
  
                if(jwtSection.Exists())
                {
                      jwtSection.Bind(jwtSettings);
                }

              configuration.Bind(JwtSettings.Section, jwtSettings);

               services.AddSingleton(Options.Create(jwtSettings));
               services.AddSingleton<IPasswordHasher, PasswordHasher>();
               services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

               services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                       .AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters{

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                         ValidAudience = jwtSettings.Audience
                        ,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            jwtSettings.Secret
                        ))

                       });

                       return services;

        }
    }
}
