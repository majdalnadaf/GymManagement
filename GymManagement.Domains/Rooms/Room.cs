using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domains.Rooms
{
    public class Room
    {
        public Guid Id { get; }
        public string Name { get; } = null!;

        public Guid GymId { get; }

        public int MaxDailySessions { get; }

        public Room(string name 
               , Guid gymId 
               , int maxDailySessions
               , Guid? id = null)
        {
            Name = name;
            GymId = gymId;
            MaxDailySessions = maxDailySessions;
            Id = id?? Guid.NewGuid();   
        }




        private Room() 
        {

        }



    }
}
