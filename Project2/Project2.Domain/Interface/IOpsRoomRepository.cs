using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Interface
{
    public interface IOpsRoomRepository
    {
        Task<IEnumerable<OpsRoom>> GetAllRoomsAsync();
        Task<OpsRoom> GetOpsRoomAsync(int? id);
        Task<IEnumerable<OpsRoom>> GetAvailableRoomsAsync();
        Task Update(OpsRoom opsRoom);
        Task SaveAsync();
    }
}
