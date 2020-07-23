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
        PatientRoom Create(PatientRoom patientRoom);
        Task Update(PatientRoom patientRoom);
        Task SaveAsync();
    }
}
