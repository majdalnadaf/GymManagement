using ErrorOr;
using GymManagement.Domains.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Queries.ListGyms
{
    public  record ListGymsQuery(
        
        Guid subscriptionId
        
        ) : IRequest<ErrorOr<List<Gym>>>;
    
}
