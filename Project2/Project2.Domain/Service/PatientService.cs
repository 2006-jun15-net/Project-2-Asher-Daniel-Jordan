using Project2.Domain.Interface;
using Project2.Domain.Model;
using System;
using System.Collections.Generic;
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

        public async Task AssignPatientToRoom(Patient patient)
        {

        }
    }
}
