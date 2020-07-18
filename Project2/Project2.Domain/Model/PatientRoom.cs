using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class PatientRoom
    {
        public int PatientRoomId { get; set; }
        public bool Available { get; set; }

        public PatientRoom(int id, bool available)
        {
            PatientRoomId = id;
            Available = available;

        }

        public PatientRoom() { }

    }
}
