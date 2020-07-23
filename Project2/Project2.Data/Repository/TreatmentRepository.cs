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

            return Entities.Select(e => new Treatment(e.IllnessId, e.DoctorId, e.Name, e.TimeToTreat));
        }

        public async Task<IEnumerable<Treatment>> GetDoctorTreatmentsAsync(int id)
        {
            var entities = await _context.TreatmentEntity
                .Where(e => e.DoctorId == id)
                .ToListAsync();

            return entities.Select(e => new Treatment(e.IllnessId, e.DoctorId, e.Name, e.TimeToTreat));

        }

        public async Task<Treatment> GetTreatmentAsync(int id)
        {
            var entity = await _context.TreatmentEntity.FindAsync(id);

            return new Treatment(entity.IllnessId, entity.DoctorId, entity.Name, entity.TimeToTreat);
        }
    }
}

