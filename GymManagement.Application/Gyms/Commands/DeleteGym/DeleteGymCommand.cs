using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.DeleteGym
{
    public  record DeleteGymCommand (
        
        Guid subscriptionId,
        Guid gymId
        
        ) :IRequest<ErrorOr<Deleted>>;
   
}
