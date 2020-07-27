using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class TreatmentEntity
    {
        public TreatmentEntity()
        {
            TreatmentDetailsEntity = new HashSet<TreatmentDetailsEntity>();
        }

        public int TreatmentId { get; set; }
        public int IllnessId { get; set; }
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public int TimeToTreat { get; set; }

        public virtual DoctorEntity Doctor { get; set; }
        public virtual IllnessEntity Illness { get; set; }
        public virtual ICollection<TreatmentDetailsEntity> TreatmentDetailsEntity { get; set; }
    }
}
