using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class TreatmentDetails
    {
        public int? OpsRoomId { get; set; }
        public int PatientId { get; set; }

        public int TreatmentId { get; set; }
        public string StartTime { get; set; }

        public TreatmentDetails(int opsroomid, int patientid, int treatmentid, string starttime)
        {
            OpsRoomId = opsroomid;
            PatientId = patientid;
            StartTime = starttime;
            TreatmentId = treatmentid;
        }

        public TreatmentDetails() { }

    }
}
