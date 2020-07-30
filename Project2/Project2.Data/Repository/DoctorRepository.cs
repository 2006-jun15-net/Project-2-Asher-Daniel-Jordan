using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project2.Data.Model;
using Project2.Domain.Interface;
using Project2.Domain.Model;

namespace Project2.Data.Repository
{
    
    public class DoctorRepository : IDoctorRepository
    {
        private readonly Project2Context _context;

        public DoctorRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsAsync()
        {
            var Entities = await _context.DoctorEntity.ToListAsync();

            return Entities.Select(e => new Doctor(e.DoctorId, e.FirstName, e.LastName));
        }

        public async Task<Doctor> GetDoctorAsync(int id)
        {
            var entity = await _context.DoctorEntity.FindAsync(id);

            if(entity == null)
            {
                return null;
            }
            else
            {
                return new Doctor(entity.DoctorId, entity.FirstName, entity.LastName);
            }
        }

        public async Task CreateDoctorAsync(Doctor doctor)
        {
            var Entity = new DoctorEntity { DoctorId = doctor.DoctorId, FirstName = doctor.FirstName, LastName = doctor.LastName };

            _context.DoctorEntity.Add(Entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {

            var entity = await _context.DoctorEntity.FindAsync(doctor.DoctorId);
            var newEntity = new DoctorEntity
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            };

            _context.Entry(entity).CurrentValues.SetValues(newEntity);

            await _context.SaveChangesAsync();

        }

        public async Task DeleteDoctorAsync(Doctor doctor)
        {
            var Entity = await _context.DoctorEntity.FindAsync(doctor.DoctorId);

            _context.DoctorEntity.Remove(Entity);

            await _context.SaveChangesAsync();
        }
    }
}
