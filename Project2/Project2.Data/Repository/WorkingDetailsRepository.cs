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
    public class WorkingDetailsRepository : IWorkingDetailsRepository
    {
        private readonly Project2Context _context;

        public WorkingDetailsRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.WorkingDetailsEntity.Find(id);

            _context.WorkingDetailsEntity.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkingDetails>> GetAllAsync()
        {
            var Entities = await _context.WorkingDetailsEntity.ToListAsync();

            return Entities.Select(e => new WorkingDetails(e.DoctorId, e.NurseId, e.ActiveAssociation));
        }

        public async Task UpdateAsync(WorkingDetails wd)
        {
            var currentEntity = _context.WorkingDetailsEntity.Where(w => w.DoctorId == wd.DoctorId && w.NurseId == wd.NurseId && w.ActiveAssociation == wd.ActiveAssociation).FirstOrDefault();
            var updatedEntity = new WorkingDetailsEntity
            {
                DoctorId = wd.DoctorId,
                NurseId = wd.NurseId,
                ActiveAssociation = wd.ActiveAssociation
            };

            _context.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);

            await _context.SaveChangesAsync();
        }
    }
}

