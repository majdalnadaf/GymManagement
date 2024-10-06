using GymManagement.Domains.Gyms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Constant = TestCommon.Constants.Constant;

namespace TestCommon.Gyms
{
    public static class GymFactory
    {

        public static Gym CreateGym(

           string name = Constant.Gym.Name,
           Guid? id = null,
           int maxRoom = Constant.Subscription.MaxRoomsFreeTier

            )
        {

            return new Gym(
                subscriptionId: Constant.Subscription.Id,
                name : name,
                maxRoom: maxRoom,
                id: id?? Constant.Gym.Id

                );


        }

    }
}
