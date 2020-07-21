using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Interface
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();


        Patient Create(Patient patient);

        void Update(Patient patient);

        IEnumerable<Patient> GetByNurse(int nurseId);

        IEnumerable<Patient> GetByDoctor(int doctorId);

    }
}
