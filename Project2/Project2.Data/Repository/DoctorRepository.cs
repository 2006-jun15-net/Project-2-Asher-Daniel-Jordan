using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<Doctor> GetAll()
        {
            var Entities = _context.DoctorEntity.ToList();

            return Entities.Select(e => new Doctor(e.DoctorId, e.FirstName, e.LastName));
        }

        public Doctor GetbyId(int id)
        {
            var entity = _context.DoctorEntity.Find(id);

            return new Doctor(entity.DoctorId, entity.FirstName, entity.LastName);
        }
    }
}
