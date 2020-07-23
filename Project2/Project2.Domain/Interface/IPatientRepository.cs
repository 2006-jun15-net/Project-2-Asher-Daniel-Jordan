using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Interface
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<Patient> CreateAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task<IEnumerable<Patient>> GetByNurseAsync(int nurseId);
        Task<IEnumerable<Patient>> GetByDoctorAsync(int doctorId);

    }
}
