using Project2.Domain.Interface;
using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Service
{
    public class PatientService
    {
        private readonly IPatientRepository patientRepository;

        private readonly IPatientRoomRepository patientRoomRepository;

        public PatientService(IPatientRepository pRepo, IPatientRoomRepository proomRepo )
        {
            patientRepository = pRepo;
            patientRoomRepository = proomRepo;
        }

        public async Task<PatientRoom> AssignPatientToRoomAsync(Patient patient)
        {
            if(patientRoomRepository.GetRoomsAsync().Result.Any(pr => pr.Available == true))
            {
                var patientRoom = patientRoomRepository.GetRoomsAsync().Result.Where(pr => pr.Available == true).FirstOrDefault();

                patient.PatientRoomId = patientRoom.PatientRoomId;

                await patientRepository.UpdateAsync(patient);

                return patientRoom;
            }
            else
            {
                var patientRoom = new PatientRoom(0, false);

                await patientRoomRepository.CreateAsync(patientRoom);

                patient.PatientRoomId = patientRoom.PatientRoomId;

                await patientRepository.UpdateAsync(patient);

                return patientRoom;


            }

            








        }
    }
}
