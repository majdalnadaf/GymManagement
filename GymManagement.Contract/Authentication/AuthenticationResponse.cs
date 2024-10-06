using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Contract.Authentication
{
    public record AuthenticationResponse(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string token
        
        );
    
}
