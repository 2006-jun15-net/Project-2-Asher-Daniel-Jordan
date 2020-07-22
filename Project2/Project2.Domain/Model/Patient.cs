using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class Patient
    {
        private string _firstName;
        private string _lastName;

        public int PatientId { get; set; }
        public int? PatientRoomId { get; set; }
        public int IllnessId { get; set; }
        public int DoctorId { get; set; }
        public string FirstName 
        {
            get => _firstName; 
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("First Name cannot be empty", nameof(value));
                }
                _firstName = value;
            }
        }
        public string LastName 
        {
            get => _lastName; 
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("Last Name cannot be empty", nameof(value));
                }
                _lastName = value;
            }
        }

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
