using ErrorOr;
using GymManagement.Domains.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.CreateGym
{
    public  record CreateGymCommand(

        string name ,
        Guid subscriptionId
        
        ) :IRequest<ErrorOr<Gym>> ;
    
}
