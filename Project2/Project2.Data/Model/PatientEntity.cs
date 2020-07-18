using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class PatientEntity
    {
        public PatientEntity()
        {
            TreatmentDetailsEntity = new HashSet<TreatmentDetailsEntity>();
        }

        public int PatientId { get; set; }
        public int? PatientRoomId { get; set; }
        public int IllnessId { get; set; }
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual DoctorEntity Doctor { get; set; }
        public virtual IllnessEntity Illness { get; set; }
        public virtual PatientRoomEntity PatientRoom { get; set; }
        public virtual ICollection<TreatmentDetailsEntity> TreatmentDetailsEntity { get; set; }
    }
}
