using Project2.Data.Model;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2.Data.Repository
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly Project2Context _context;

        public TreatmentRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Treatment> GetAll()
        {
            var Entities = _context.TreatmentEntity.ToList();


            return Entities.Select(e => new Treatment(e.IllnessId, e.DoctorId, e.Name));
        }

        public IEnumerable<Treatment> GetAllByDoctor(int id)
        {
            var entities = _context.TreatmentEntity.ToList();
            var filteredEntities = entities.Where(e => e.DoctorId == id).ToList();

            return filteredEntities.Select(e => new Treatment(e.IllnessId, e.DoctorId, e.Name, e.TimeToTreat));

        }
    }
}

