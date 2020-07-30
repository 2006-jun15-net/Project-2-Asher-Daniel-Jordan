using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Interface
{
    public interface INurseRepository
    {
        Task<IEnumerable<Nurse>> GetNursesAsync();

        Task<Nurse> GetByNurseIdAsync(int nureseId);

        Task CreateNurseAsync(Nurse nurse);

        Task UpdateNurseAsync(Nurse nurse);

        Task DeleteNurseAsync(Nurse nurse);
    }
}
