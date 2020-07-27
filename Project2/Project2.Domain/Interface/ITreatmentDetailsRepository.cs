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

        Task<TreatmentDetails> GetByIdAsync();

        Task<TreatmentDetails> CreateAsync(TreatmentDetails td);

        Task UpdateAsync(TreatmentDetails td);

        Task DeleteAsync(TreatmentDetails td);
    }
}
