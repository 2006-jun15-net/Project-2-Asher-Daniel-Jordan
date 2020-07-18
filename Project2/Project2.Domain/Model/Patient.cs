using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class Patient
    {
        public int PatientId { get; set; }
        public int? PatientRoomId { get; set; }
        public int IllnessId { get; set; }
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Patient(int patientid, int? patientroomid, int illnessid, string firstname, string lastname)
        {
            PatientId = patientid;
            PatientRoomId = patientroomid;
            IllnessId = illnessid;
            FirstName = firstname;
            LastName = lastname;
        }

        public Patient() { }


    }
}
