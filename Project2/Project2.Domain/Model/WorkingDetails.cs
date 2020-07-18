using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain.Model
{
    public class WorkingDetails
    {

        public int NurseId { get; set; }
        public int DoctorId { get; set; }
        public bool ActiveAssociation { get; set; }

        public WorkingDetails(int nurseid, int doctorid, bool activeassociation)
        {
            NurseId = nurseid;
            DoctorId = doctorid;
            ActiveAssociation = activeassociation;
        }

        public WorkingDetails() { }

    }
}
