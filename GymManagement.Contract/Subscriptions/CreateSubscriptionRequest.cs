using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GymManagement.Contract.Subscriptions
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SubscriptionType
    {
        Free,
        Starter,
        Pro

    }

    public class CreateSubscriptionRequest
    {
        public Guid AdminId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
    }
}
