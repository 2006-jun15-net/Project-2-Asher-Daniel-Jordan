using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class NurseEntity
    {
        public NurseEntity()
        {
            WorkingDetailsEntity = new HashSet<WorkingDetailsEntity>();
        }

        public int NurseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<WorkingDetailsEntity> WorkingDetailsEntity { get; set; }
    }
}
