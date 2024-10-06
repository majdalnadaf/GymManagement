using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domains.Subscriptions
{
    public sealed class SubscriptionType : SmartEnum<SubscriptionType,int>
    {

        public static readonly  SubscriptionType Free = new(nameof(Free), 0);
        public static readonly SubscriptionType Starter = new(nameof(Starter), 1);
        public static readonly SubscriptionType Pro = new(nameof(Pro), 2);
        private SubscriptionType(string name, int value) : base(name, value)
        {

        }
    }
}
