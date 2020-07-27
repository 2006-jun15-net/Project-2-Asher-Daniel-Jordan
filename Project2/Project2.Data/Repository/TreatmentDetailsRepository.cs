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
    public class TreatmentDetailsRepository : ITreatmentDetailsRepository
    {
        private readonly Project2Context _context;

        public TreatmentDetailsRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TreatmentDetails> CreateAsync(TreatmentDetails td)
        {
            var tdEntity = new TreatmentDetailsEntity
            {
                StartTime = td.StartTime,
                PatientId = td.PatientId,
                OpsRoomId = td.OpsRoomId,
                TreatmentId = td.TreatmentId
            };

            _context.TreatmentDetailsEntity.Add(tdEntity);

            await _context.SaveChangesAsync();

            return td;
        }

        public Task DeleteAsync(TreatmentDetails td)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TreatmentDetails>> GetAllAsync()
        {
            var Entities = await _context.TreatmentDetailsEntity.ToListAsync();

            return Entities.Select(e => new TreatmentDetails((int)e.OpsRoomId, e.PatientId, e.TreatmentId, e.StartTime));
        }

        public Task<TreatmentDetails> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TreatmentDetails td)
        {
            throw new NotImplementedException();
        }
    }
}

