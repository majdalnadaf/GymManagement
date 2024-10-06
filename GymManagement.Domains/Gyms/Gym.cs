using ErrorOr;
using GymManagement.Domains.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domains.Gyms
{
    public class Gym
    {
        
        public Guid Id { get;  }

        private readonly int _maxRoom;
        public string Name { get; init; }    
        public Guid SubscriptionId { get; init; }
        private readonly List<Guid> _roomIds = new();
        private readonly List<Guid> _trainerIds = new();

        public Gym(
            Guid subscriptionId ,
            string name , 
            int maxRoom , 
            Guid? id = null )
        {
            Name = name;
            SubscriptionId = subscriptionId;
            _maxRoom = maxRoom;
            Id = id?? Guid.NewGuid();

        }


        public ErrorOr<Success> AddRoom(Room room)
        {
            _roomIds.Add(room.Id);
            return Result.Success;
        }


        public bool HasRoom (Guid roomId)
        {
            return _roomIds.Contains(roomId);
        }


        public ErrorOr<Success> RemoveRoom(Guid roomId)
        {
             _trainerIds.Remove(roomId);
             return Result.Success; 
           
        }

        public ErrorOr<Success> AddTrainer(Guid trainerId)
        {
            _trainerIds.Add(trainerId);
            return Result.Success;
        }

        public bool HasTrainer(Guid trainerId)
        {
            return _trainerIds.Contains(trainerId);
        }


        private Gym()
        {

        }


    }
}
