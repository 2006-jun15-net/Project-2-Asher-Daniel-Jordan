using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class PatientRoomEntity
    {
        public PatientRoomEntity()
        {
            PatientEntity = new HashSet<PatientEntity>();
        }

        public int PatientRoomId { get; set; }
        public bool Available { get; set; }

        public virtual ICollection<PatientEntity> PatientEntity { get; set; }
    }
}
