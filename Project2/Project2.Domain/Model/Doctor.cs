using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Doctor(int id, string firstname, string lastname)
        {
            DoctorId = id;
            FirstName = firstname;
            LastName = lastname;
        }

        public Doctor() { }


    }
}
