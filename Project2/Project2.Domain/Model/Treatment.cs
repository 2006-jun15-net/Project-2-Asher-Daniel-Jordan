using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class Treatment
    {
        public int IllnessId { get; set; }
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public int TimeToTreat { get; set; }

        public Treatment(int illnessid, int doctorid, string name, int time)
        {
            IllnessId = illnessid;
            DoctorId = doctorid;
            Name = name;
            TimeToTreat = time;
        }

        public Treatment() { }



    }
}
