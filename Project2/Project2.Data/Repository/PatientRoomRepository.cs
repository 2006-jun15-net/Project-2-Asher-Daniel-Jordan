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
    public class PatientRoomRepository : IPatientRoomRepository
    {
        private readonly Project2Context _context;

        public PatientRoomRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(PatientRoom patientRoom)
        {
            var prEntity = new PatientRoomEntity
            {
                PatientRoomId = _context.PatientRoomEntity.Last().PatientRoomId + 1,
                Available = patientRoom.Available
            };

            _context.PatientRoomEntity.Add(prEntity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PatientRoom>> GetRoomsAsync()
        {
            var Entities = await _context.PatientRoomEntity.ToListAsync();

            return Entities.Select(e => new PatientRoom(e.PatientRoomId, e.Available));
        }

        public async Task<PatientRoom> GetRoomAsync(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException("ID cannot be null", nameof(id));
            }

            var entity = await _context.PatientRoomEntity.FindAsync(id);
            if(entity == null)
            {
                return null;
            }
            return new PatientRoom(entity.PatientRoomId, entity.Available);
        }

        public async Task Update(PatientRoom patientRoom)
        {
            var entity = await _context.PatientRoomEntity.FindAsync(patientRoom.PatientRoomId);

            var newEntity = new PatientRoomEntity
            {
                PatientRoomId = patientRoom.PatientRoomId,
                Available = patientRoom.Available,
            };

            _context.Entry(entity).CurrentValues.SetValues(newEntity);
        }

        public async Task DeleteAsync(PatientRoom patientRoom)
        {
            var pRoomEntity = _context.PatientRoomEntity.Find(patientRoom.PatientRoomId);

            _context.PatientRoomEntity.Remove(pRoomEntity);

            await _context.SaveChangesAsync();
        }
    }
}

