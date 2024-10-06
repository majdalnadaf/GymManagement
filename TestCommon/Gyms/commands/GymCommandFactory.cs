using GymManagement.Application.Gyms.Commands.CreateGym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TestCommon.Constants;

namespace TestCommon.Gyms.commands
{
    public static class GymCommandFactory
    {

        public static CreateGymCommand CreateCreateGymCommand(
            string name = Constant.Gym.Name,
            Guid? subscriptionId = null
            
            )
        {

            return new CreateGymCommand(
                name: name,
                subscriptionId: subscriptionId ?? Constant.Subscription.Id
                );

        }
    }
}
