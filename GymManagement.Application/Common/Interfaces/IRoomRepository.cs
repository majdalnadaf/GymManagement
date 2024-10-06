using GymManagement.Domains.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface IRoomRepository
    {
        Task AddRoomAsync(Room room);

        Task UpdateRoomAsync(Room room);

        Task<Room> GetByIdAsync(Guid id);

        Task RemoveRoomAsync(Room room);

        Task RemoveRangeAsync(List<Room> rooms);
        Task<List<Room>> ListByGymIdAsync(Guid gymId);

    }
}
