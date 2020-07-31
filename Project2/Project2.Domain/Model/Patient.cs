using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class Patient : IEquatable<Patient>
    {
        private string _firstName;
        private string _lastName;

        public int PatientId { get; set; }
        public int? PatientRoomId { get; set; }
        
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

        public Patient(int patientid, int? patientroomid,  string firstname, string lastname)
        {
            PatientId = patientid;
            PatientRoomId = patientroomid;
            FirstName = firstname;
            LastName = lastname;
        }

        public Patient() { }

        public bool Equals(Patient other)
        {
            //Check whether the compared object is null.
            if (ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return PatientId.Equals(other.PatientId) && FirstName.Equals(other.FirstName);
        }
    }
}
