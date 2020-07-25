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
            var Entity = new PatientEntity { DoctorId = patient.DoctorId, FirstName = patient.FirstName, LastName = patient.LastName };

            _context.PatientEntity.Add(Entity);

            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            var Entities = await _context.PatientEntity.ToListAsync();

            return Entities.Select(e => new Patient(e.PatientId, e.PatientRoomId,e.IllnessId, e.DoctorId, e.FirstName, e.LastName));
        }


        public async Task UpdateAsync(Patient patient)
        {
            PatientEntity currentPatient = await _context.PatientEntity.FindAsync(patient.PatientId);
            var Entity = new PatientEntity
            { 
                FirstName = patient.FirstName,
                LastName = patient.LastName, 
                DoctorId = patient.DoctorId,
                PatientRoomId = patient.PatientRoomId 
            };

            _context.Entry(currentPatient).CurrentValues.SetValues(Entity);

            await _context.SaveChangesAsync();
         }

        public async Task<IEnumerable<Patient>> GetByDoctorAsync(int doctorId)
        {
            var entities = await _context.PatientEntity
                .Where(e => e.DoctorId == doctorId)
                .ToListAsync();
            return entities.Select(e => new Patient(e.PatientId, e.PatientRoomId, e.IllnessId, e.DoctorId, e.FirstName, e.LastName));

        }

        public async Task<IEnumerable<Patient>> GetByNurseAsync(int nurseId)
        {
            List <PatientEntity> filteredEntities = new List<PatientEntity>();

            List<int> doctorIds = await _context.WorkingDetailsEntity
                    .Where(wd => wd.NurseId == nurseId)
                    .Select(wd => wd.DoctorId)
                    .ToListAsync();

            foreach (var id in doctorIds)
            {
                filteredEntities.AddRange( await _context.PatientEntity.Where(p => p.DoctorId == id).ToListAsync());
            }


            return filteredEntities.Select(e => new Patient(e.PatientId, e.PatientRoomId, e.IllnessId, e.DoctorId, e.FirstName, e.LastName));

        }
    }
}
