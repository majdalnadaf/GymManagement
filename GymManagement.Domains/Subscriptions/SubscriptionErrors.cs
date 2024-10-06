using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domains.Subscriptions
{
    public class SubscriptionErrors
    {
        public static readonly Error CannotHaveMoreGymsThanSubscriptionAllows = Error.Validation(
            code: "Subscription.CannotHaveMoreGymsThanSubscriptionAllows ",
            description: "A subscription cannot have more gyms than subscription allows"
            );

    }
}
