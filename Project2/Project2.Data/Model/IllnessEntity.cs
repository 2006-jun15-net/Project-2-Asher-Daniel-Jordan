using System;
using System.Collections.Generic;

namespace Project2.Data.Model
{
    public partial class IllnessEntity
    {
        public IllnessEntity()
        {
            TreatmentEntity = new HashSet<TreatmentEntity>();
        }

        public int IllnessId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TreatmentEntity> TreatmentEntity { get; set; }
    }
}
