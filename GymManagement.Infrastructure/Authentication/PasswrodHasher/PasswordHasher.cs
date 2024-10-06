using ErrorOr;
using GymManagement.Domains.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using BCrypt.Net;

namespace GymManagement.Infrastructure.Authentication.PasswrodHasher
{
    internal partial class PasswordHasher : IPasswordHasher
    {
        private static readonly Regex _passwordRegex = StrongPasswordRegex();

        public ErrorOr<string> HashPassword(string password)
        {

            var result =  _passwordRegex.IsMatch(password);


                 return result ? BCrypt.Net.BCrypt.EnhancedHashPassword(password )
                   : Error.Validation(description: "password is to weak");

        }

        public bool IsCorrectPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
        }



        [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled)]
        private static partial Regex StrongPasswordRegex();

    }
}
