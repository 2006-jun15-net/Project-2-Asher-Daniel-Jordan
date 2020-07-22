using Project2.Data.Model;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2.Data.Repository
{
    public class OpsRoomRepository : IOpsRoomRepository
    {
        private readonly Project2Context _context;

        public OpsRoomRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<OpsRoom> GetAll()
        {
            var Entities = _context.OpsRoomEntity.ToList();

            return Entities.Select(e => new OpsRoom(e.OpsRoomId, e.Available));
        }

        public IEnumerable<OpsRoom> GetAvailableRooms()
        {
            var entities = _context.OpsRoomEntity.ToList();
            var filteredEntities = entities.Where(e => e.Available == true).ToList();

            return filteredEntities.Select(e => new OpsRoom(e.OpsRoomId, e.Available));
        }
    }
}
