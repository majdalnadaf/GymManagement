using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Users;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using GymManagement.Infrastructure.Authentication.ClaimExctension;


namespace GymManagement.Infrastructure.Authentication.TokenGenerator
{
    internal class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }   

        public string GenerateToken(User user)
        {
           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

           var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           
           var claims = new List<Claim>
           {
              new(JwtRegisteredClaimNames.Name, user.FirstNamme),
              new(JwtRegisteredClaimNames.FamilyName,user.LastName),
              new(JwtRegisteredClaimNames.Email, user.Email),
              new("id",user.Id.ToString()),
              new("permissions","gyms:create"),  
              new("permissions","gyms:update")

           };

           
           AddIds(user, claims);

            var token  = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
                signingCredentials:credentials
                                                                                             
            );
             
             return new JwtSecurityTokenHandler().WriteToken(token);


        }


    private static void AddIds(User user, List<Claim> claims)
    {
         claims
         .AddIfValueNotNull("adminId", user.AdminId?.ToString() )
        .AddIfValueNotNull("participantId", user.ParticipantId?.ToString())
        .AddIfValueNotNull("trainerId",user.TrainerId?.ToString());
    }




    }  
}
