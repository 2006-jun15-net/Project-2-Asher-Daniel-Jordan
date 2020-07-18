using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class TreatmentEntity
    {
        public int IllnessId { get; set; }
        public int DoctorId { get; set; }
        public string Name { get; set; }

        public virtual DoctorEntity Doctor { get; set; }
        public virtual IllnessEntity Illness { get; set; }
    }
}
