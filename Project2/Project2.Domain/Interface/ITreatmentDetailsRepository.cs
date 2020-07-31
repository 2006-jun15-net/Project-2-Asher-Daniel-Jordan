using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Interface
{
    public interface ITreatmentDetailsRepository
    {
        Task<IEnumerable<TreatmentDetails>> GetAllAsync();

        Task<TreatmentDetails> GetByIdAsync(int id);

        Task CreateAsync(TreatmentDetails td);

        Task UpdateAsync(TreatmentDetails td);

        Task DeleteAsync(TreatmentDetails td);

        Task<IEnumerable<TreatmentDetails>> GetPatientTreatment(int id);

        Task<IEnumerable<TreatmentDetails>> GetByDoctorAsync(int doctorId);
    }
}
