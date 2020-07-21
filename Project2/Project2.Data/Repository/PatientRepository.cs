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

        public Patient Create(Patient patient)
        {
            var Entity = new PatientEntity { DoctorId = patient.DoctorId, FirstName = patient.FirstName, LastName = patient.LastName };

            _context.PatientEntity.Add(Entity);

            _context.SaveChanges();

            return patient;
        }

        public IEnumerable<Patient> GetAll()
        {
            var Entities = _context.PatientEntity.ToList();

            return Entities.Select(e => new Patient(e.PatientId, e.PatientRoomId, e.DoctorId, e.FirstName, e.LastName));
        }

        public void Update(Patient patient)
        {
            PatientEntity currentPatient = _context.PatientEntity.Find(patient.PatientId);
            var Entity = new PatientEntity { FirstName = patient.FirstName, LastName = patient.LastName, DoctorId = patient.DoctorId, PatientRoomId = patient.PatientRoomId };

            _context.Entry(currentPatient).CurrentValues.SetValues(Entity);
            _context.SaveChanges();
        }
    }
}
