using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


using GymManagement.Domains.Subscriptions;
using TestCommon.Constants;
using Constant = TestCommon.Constants.Constant;

namespace TestCommon.Subscriptions
{
    public static class SubscriptionFactory
    {
        public static Subscription CreateSubscription(
            SubscriptionType subscriptionType = null,
            Guid? adminId = null,
            Guid? id = null
            
            )
        {


            return new Subscription(
                subscriptionType: subscriptionType ?? Constant.Subscription.DefaultSubscriptionType,
                adminId: adminId ?? Constant.Admin.Id,
                id: id ?? Constant.Subscription.Id

                );

        }

    }
}
