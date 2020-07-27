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
    public class PatientRepository : IPatientRepository
    {
        private readonly Project2Context _context;

        public PatientRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            var Entity = new PatientEntity { PatientRoomId = patient.PatientRoomId, FirstName = patient.FirstName, LastName = patient.LastName };

            _context.PatientEntity.Add(Entity);

            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            var Entities = await _context.PatientEntity.ToListAsync();

            return Entities.Select(e => new Patient(e.PatientId, e.PatientRoomId,  e.FirstName, e.LastName));
        }


        public async Task UpdateAsync(Patient patient)
        {
            PatientEntity currentPatient = await _context.PatientEntity.FindAsync(patient.PatientId);
            var Entity = new PatientEntity
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PatientRoomId = patient.PatientRoomId
            };

            _context.Entry(currentPatient).CurrentValues.SetValues(Entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Patient>> GetByDoctorAsync(int doctorId)
        {
            var treatmentIds = await _context.TreatmentEntity
                .Where(t => t.DoctorId == doctorId)
                .Select(t => t.TreatmentId)
                .ToListAsync();

            List<int> patientIds = new List<int>();
            foreach(int id in treatmentIds)
            {
                patientIds.AddRange( _context.TreatmentDetailsEntity
                     .Where(td => td.TreatmentId == id)
                     .Select(td => td.TreatmentId)
                     .ToList());
            }

            List<PatientEntity> patientEntities = new List<PatientEntity>();
            foreach(int id in patientIds)
            {
                patientEntities.AddRange(_context.PatientEntity.Where(p => p.PatientId == id).ToList());
            }
                
            return patientEntities.Select(e => new Patient(e.PatientId, e.PatientRoomId,  e.FirstName, e.LastName));

        }

        public async Task<IEnumerable<Patient>> GetByNurseAsync(int nurseId)
        {
            List<int> treatmentIds = new List<int>();

            List < PatientEntity > patientEntities = new List<PatientEntity>();

            List<int> doctorIds = await _context.WorkingDetailsEntity
                    .Where(wd => wd.NurseId == nurseId)
                    .Select(wd => wd.DoctorId)
                    .ToListAsync();

            foreach (var id in doctorIds)
            {
                treatmentIds.AddRange( _context.TreatmentEntity.Where(t => t.DoctorId == id).Select(t => t.DoctorId).ToList());
            }

            foreach(var id in treatmentIds)
            {
                patientEntities.AddRange( _context.PatientEntity.Where(p => p.PatientId == id).ToList());

            }


            return patientEntities.Select(e => new Patient(e.PatientId, e.PatientRoomId,  e.FirstName, e.LastName));

        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            var patientEntitiy = await _context.PatientEntity.FindAsync(id);

            return new Patient
                (
                patientEntitiy.PatientId,
                patientEntitiy.PatientRoomId,
                patientEntitiy.FirstName,
                patientEntitiy.LastName
                );
        }

        public async Task DeletePatientAsync(Patient patient)
        {
            var patientEntiy = new PatientEntity
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PatientRoomId = patient.PatientRoomId,
                
            };

            _context.PatientEntity.Remove(patientEntiy);

            await _context.SaveChangesAsync();
            
        }
    }
}
