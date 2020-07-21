using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Interface
{
    public interface IPatientRoomRepository
    {
        IEnumerable<PatientRoom> GetAll();

        PatientRoom Create(PatientRoom patientRoom);

        PatientRoom Update(PatientRoom patientRoom);
    }
}
