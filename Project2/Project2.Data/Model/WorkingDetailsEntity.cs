using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class WorkingDetailsEntity
    {
        public int NurseId { get; set; }
        public int DoctorId { get; set; }
        public bool ActiveAssociation { get; set; }

        public virtual DoctorEntity Doctor { get; set; }
        public virtual NurseEntity Nurse { get; set; }
    }
}
