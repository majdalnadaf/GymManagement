using GymManagement.Domains.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Constant = TestCommon.Constants.Constant;

namespace TestCommon.Admins
{
    public static class AdminFactory
    {

        public static Admin CreateAdmin()
        {
            return new Admin(
                userId: Constant.Subscription.Id,
                subscriptionId: Constant.Subscription.Id,
                id: Constant.Admin.Id

                );
        }
    }
}
