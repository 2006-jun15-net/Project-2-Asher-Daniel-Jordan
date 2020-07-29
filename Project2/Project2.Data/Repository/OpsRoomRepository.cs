using Microsoft.EntityFrameworkCore;
using Project2.Data.Model;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Data.Repository
{
    public class OpsRoomRepository : IOpsRoomRepository
    {
        private readonly Project2Context _context;

        public OpsRoomRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<OpsRoom>> GetAllRoomsAsync()
        {
            var entities = await _context.OpsRoomEntity.ToListAsync();

            return entities.Select(e => new OpsRoom(e.OpsRoomId, e.Available));
        }

        public async Task<OpsRoom> GetOpsRoomAsync(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException("ID cannot be null", nameof(id));
            }

            var entity = await _context.OpsRoomEntity.FindAsync(id);

            return new OpsRoom(entity.OpsRoomId, entity.Available);
        }

        public async Task<IEnumerable<OpsRoom>> GetAvailableRoomsAsync()
        {
            var entities = await _context.OpsRoomEntity.Where(e => e.Available == true).ToListAsync();

            return entities.Select(e => new OpsRoom(e.OpsRoomId, e.Available));
        }

        public async Task Update(OpsRoom opsRoom)
        {
            var entity = await _context.OpsRoomEntity.FindAsync(opsRoom.OpsRoomId);
            var newEntity = new OpsRoomEntity
            {
                OpsRoomId = opsRoom.OpsRoomId,
                Available = opsRoom.Available
            };

            _context.Entry(entity).CurrentValues.SetValues(newEntity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<OpsRoom> CreateOpsRoomAsync(OpsRoom opsRoom)
        {
            var opsroomEntity = new OpsRoomEntity 
            { 
                OpsRoomId = opsRoom.OpsRoomId,
                Available = opsRoom.Available,
                
            };

            _context.OpsRoomEntity.Add(opsroomEntity);

            await SaveAsync();

            return opsRoom;
        }

        public async Task DeleteAsync(OpsRoom opsRoom)
        {
            var opsRoomEntity = _context.OpsRoomEntity.Find(opsRoom.OpsRoomId);

            _context.OpsRoomEntity.Remove(opsRoomEntity);

            await SaveAsync();
           
        }
    }
}
