using Project2.Data.Model;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2.Data.Repository
{
    public class PatientRoomRepository : IPatientRoomRepository
    {
        private readonly Project2Context _context;

        public PatientRoomRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public PatientRoom Create(PatientRoom patientRoom)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PatientRoom> GetAll()
        {
            var Entities = _context.PatientRoomEntity.ToList();

            return Entities.Select(e => new PatientRoom(e.PatientRoomId, e.Available));
        }

        public PatientRoom Update(PatientRoom patientRoom)
        {
            throw new NotImplementedException();
        }
    }
}

