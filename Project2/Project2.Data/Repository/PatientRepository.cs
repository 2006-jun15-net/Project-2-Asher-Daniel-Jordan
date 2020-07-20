using Microsoft.EntityFrameworkCore;
using Project2.Data.Model;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2.Data.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly Project2Context _context;

        public PatientRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Patient> GetAll()
        {
            var Entities = _context.PatientEntity.ToList();

            return Entities.Select(e => new Patient(e.PatientId, e.PatientRoomId, e.DoctorId, e.FirstName, e.LastName));
        }

        public IEnumerable<Patient> GetByDoctor(int doctorId)
        {
            var entities = _context.PatientEntity.ToList();
            var filteredEntities = entities.Where(e => e.DoctorId == doctorId).ToList();
            return filteredEntities.Select(e => new Patient(e.PatientId, e.PatientRoomId, e.DoctorId, e.FirstName, e.LastName));
        }

        public IEnumerable<Patient> GetByNurse(int nurseId)
        {
            List <PatientEntity> filteredEntities = new List<PatientEntity>();
            List<int> doctorIds = _context.WorkingDetailsEntity
                    .Where(wd => wd.NurseId == nurseId)
                    .Select(wd => wd.DoctorId)
                    .ToList();
            foreach (var id in doctorIds)
            {
                filteredEntities.AddRange( _context.PatientEntity.Where(p => p.DoctorId == id).ToList());
            }


            return filteredEntities.Select(e => new Patient(e.PatientId, e.PatientRoomId, e.DoctorId, e.FirstName, e.LastName));
        }
    }
}
