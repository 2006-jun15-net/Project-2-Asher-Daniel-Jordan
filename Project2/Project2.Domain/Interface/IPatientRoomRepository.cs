using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Interface
{
    public interface IPatientRoomRepository
    {
        Task<IEnumerable<PatientRoom>> GetRoomsAsync();
        Task<PatientRoom> GetRoomAsync(int? id);
        Task CreateAsync(PatientRoom patientRoom);

        Task DeleteAsync(PatientRoom patientRoom);
        Task Update(PatientRoom patientRoom);
    }
}
