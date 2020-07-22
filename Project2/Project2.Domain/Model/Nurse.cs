using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class Nurse
    {
        private string _firstName;
        private string _lastName;



        public int NurseId { get; set; }
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentNullException("Nurse First Name must have value", nameof(value));
                }
                _firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentNullException("Nurse Last Name must have value", nameof(value));

                }
                _lastName = value;
            }
        }


        public Nurse(int id, string firstname, string lastname)
        {
            NurseId = id;
            FirstName = firstname;
            LastName = lastname;
        }

        public Nurse() { }


    }
}
