using ErrorOr;
using GymManagement.Domains.Gyms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domains.Subscriptions
{
    public class Subscription
    {
        private readonly List<Guid> _gymIds = new();
        private readonly int _maxGyms;

        public Guid _adminId { get; } 
        public Guid Id { get; private set; }

        public SubscriptionType SubscriptionType { get; private set; }

        public Subscription( SubscriptionType subscriptionType , Guid adminId,Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            SubscriptionType = subscriptionType;
            _adminId = adminId;
            _maxGyms = GetMaxGyms();
        }


        public ErrorOr<Success> AddGym(Gym gym)
        {
            //thorw exception if the gym id exists in the list of gyms
           
            if(_gymIds.Count >= _maxGyms)
            {
                return SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows;
            }

            _gymIds.Add(gym.Id);
            return Result.Success;
        }


        public int GetMaxGyms() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 1,
            nameof(SubscriptionType.Pro) => 3,
            _ => throw new InvalidOperationException()
        }; 

        public int GetMaxRooms() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 3,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException()

        };

        public int GetMaxDailySessions() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 4,
            nameof(SubscriptionType.Starter) => int.MaxValue,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException()

        };

        public bool HasGym(Guid gymId)
        {
            return _gymIds.Contains(gymId);
        }

        public void RemoveGym(Guid gymId)
        {
            _gymIds.Remove(gymId);
            return;
        }

        // For entity framework
        private Subscription() 
        {

        }
    }
}
