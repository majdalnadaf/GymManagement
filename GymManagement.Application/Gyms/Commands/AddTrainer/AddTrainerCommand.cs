using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.AddTrainer
{
    public record AddTrainerCommand (

          Guid subscriptionId,
          Guid gymId,
          Guid trainerId
        
        ) : IRequest<ErrorOr<Success>> ;
    
}
