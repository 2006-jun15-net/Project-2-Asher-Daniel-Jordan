using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class Nurse
    {

        public int NurseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public Nurse(int id, string firstname, string lastname)
        {
            NurseId = id;
            FirstName = firstname;
            LastName = lastname;
        }

        public Nurse() { }


    }
}
