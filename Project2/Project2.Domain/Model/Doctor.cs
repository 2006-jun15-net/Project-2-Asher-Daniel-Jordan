using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class Doctor
    {
        private string _firstName;
        private string _lastName;


        public int DoctorId { get; set; }
        public string FirstName 
        {
            get => _firstName;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("First name cannot be empty", nameof(value));
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
                    throw new ArgumentException("Last name cannot be empty", nameof(value));
                }
                _lastName = value;
            }
        }

        public Doctor(int id, string firstname, string lastname)
        {
            DoctorId = id;
            FirstName = firstname;
            LastName = lastname;
        }

        public Doctor() { }


    }
}
