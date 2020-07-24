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

        public async Task<IEnumerable<TreatmentDetails>> GetAllAsync()
        {
            var Entities = await _context.TreatmentDetailsEntity.ToListAsync();

            return Entities.Select(e => new TreatmentDetails(e.PatientId, e.OpsRoomId, e.StartTime));
        }
    }
}

