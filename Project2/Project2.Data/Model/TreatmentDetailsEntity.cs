﻿using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class TreatmentDetailsEntity
    {
        public int OpsRoomId { get; set; }
        public int PatientId { get; set; }
        public string StartTime { get; set; }

        public virtual OpsRoomEntity OpsRoom { get; set; }
        public virtual PatientEntity Patient { get; set; }
    }
}
