﻿using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class OpsRoomEntity
    {
        public OpsRoomEntity()
        {
            TreatmentDetailsEntity = new HashSet<TreatmentDetailsEntity>();
        }

        public int OpsRoomId { get; set; }
        public bool Available { get; set; }

        public virtual ICollection<TreatmentDetailsEntity> TreatmentDetailsEntity { get; set; }
    }
}
