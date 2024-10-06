using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domains.Common.Interfaces
{
    public interface IPasswordHasher
    {

        bool IsCorrectPassword(string password, string hash);

        public ErrorOr<string> HashPassword(string password);


    }
}
