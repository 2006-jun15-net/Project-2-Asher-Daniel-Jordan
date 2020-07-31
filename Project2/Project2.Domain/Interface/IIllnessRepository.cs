using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Interface
{
     public interface IIllnessRepository
    {
        Task<IEnumerable<Illness>> GetAllAsync();

        Task<Illness> GetByIdAsync(int id);

        Task CreateAsync(Illness illness);

        Task UpdateIllnessAsync(Illness illness);

        Task DeleteIllnessAsync(Illness illness);
    }
}
