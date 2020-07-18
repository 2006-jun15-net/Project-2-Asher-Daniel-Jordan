using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class OpsRoom
    {
        public int OpsRoomId { get; set; }
        public bool Available { get; set; }

        public OpsRoom(int id, bool available)
        {
            OpsRoomId = id;
            Available = available;
        }

        public OpsRoom() { }



    }
}
