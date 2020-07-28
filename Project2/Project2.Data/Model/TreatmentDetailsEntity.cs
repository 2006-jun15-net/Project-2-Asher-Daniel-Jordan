using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class TreatmentDetailsEntity
    {
        public int TreatmentDetailsId { get; set; }
        public int? OpsRoomId { get; set; }
        public int PatientId { get; set; }
        public int TreatmentId { get; set; }
        public string StartTime { get; set; }

        public virtual PatientEntity Patient { get; set; }
        public virtual TreatmentEntity Treatment { get; set; }
    }
}
