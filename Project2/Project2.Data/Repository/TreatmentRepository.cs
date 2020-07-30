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
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly Project2Context _context;

        public TreatmentRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Treatment>> GetTreatmentsAsync()
        {
            var Entities = await _context.TreatmentEntity.ToListAsync();


            return Entities.Select(e => new Treatment(e.TreatmentId, e.IllnessId, e.DoctorId, e.Name, e.TimeToTreat));
        }

        public async Task<IEnumerable<Treatment>> GetDoctorTreatmentsAsync(int id)
        {
            var entities = await _context.TreatmentEntity
                .Where(e => e.DoctorId == id)
                .ToListAsync();

            return entities.Select(e => new Treatment(e.TreatmentId, e.IllnessId, e.DoctorId, e.Name, e.TimeToTreat));

        }

        public async Task<IEnumerable<Treatment>> TreatmentsByIlllnessAsync(int doctorId, int illnessId)
        {
            var entities = await _context.TreatmentEntity
                .Where(e => e.IllnessId == illnessId && e.DoctorId == doctorId)
                .ToListAsync();

            return entities.Select(e => new Treatment(e.TreatmentId, e.IllnessId, e.DoctorId, e.Name, e.TimeToTreat));
        }

        public async Task<Treatment> GetTreatmentAsync(int id)
        {
            var entity = await _context.TreatmentEntity.FindAsync(id);
            if(entity == null)
            {
                return null;
            }

            return new Treatment(
                entity.TreatmentId, 
                entity.IllnessId, 
                entity.DoctorId, 
                entity.Name, 
                entity.TimeToTreat
                );
        }

        public async Task CreateTreatmentAsync(Treatment t)
        {
            var entity = new TreatmentEntity
            {
                Name = t.Name,
                TimeToTreat = t.TimeToTreat,
                IllnessId = t.IllnessId
            };

            _context.TreatmentEntity.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateTreatmentAsync(Treatment t)
        {
            var currentEntity = _context.TreatmentEntity.Find(t.TreatmentId);
            var updatedEntity = new TreatmentEntity
            {
                TreatmentId = currentEntity.TreatmentId,
                Name = currentEntity.Name,
                TimeToTreat = currentEntity.TimeToTreat,
                IllnessId = currentEntity.IllnessId,
                DoctorId = currentEntity.DoctorId
            };

            _context.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTreatmentAsync(int id)
        {
           var entity = _context.TreatmentEntity.Find(id);

            _context.TreatmentEntity.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}

