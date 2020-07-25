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

            return new Doctor(entity.DoctorId, entity.FirstName, entity.LastName);
        }

        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
            var Entity = new DoctorEntity { DoctorId = doctor.DoctorId, FirstName = doctor.FirstName, LastName = doctor.LastName };

            _context.DoctorEntity.Add(Entity);

            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<Doctor> UpdateDoctorAsync(Doctor doctor)
        {

            var Entity = new DoctorEntity 
            { 
                DoctorId = doctor.DoctorId, 
                FirstName = doctor.FirstName, 
                LastName = doctor.LastName
            };

            _context.Entry(Entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return doctor;

            
        }

        public async Task<Doctor> DeleteDoctorAsync(Doctor doctor)
        {
            var Entity = new DoctorEntity 
            { 
                DoctorId = doctor.DoctorId, 
                FirstName = doctor.FirstName, 
                LastName = doctor.LastName 
            };

            _context.DoctorEntity.Remove(Entity);

            await _context.SaveChangesAsync();

            return doctor;
        }
    }
}
