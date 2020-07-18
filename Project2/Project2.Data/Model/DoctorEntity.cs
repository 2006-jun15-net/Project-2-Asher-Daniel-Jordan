using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class DoctorEntity
    {
        public DoctorEntity()
        {
            PatientEntity = new HashSet<PatientEntity>();
            TreatmentEntity = new HashSet<TreatmentEntity>();
            WorkingDetailsEntity = new HashSet<WorkingDetailsEntity>();
        }

        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<PatientEntity> PatientEntity { get; set; }
        public virtual ICollection<TreatmentEntity> TreatmentEntity { get; set; }
        public virtual ICollection<WorkingDetailsEntity> WorkingDetailsEntity { get; set; }
    }
}
