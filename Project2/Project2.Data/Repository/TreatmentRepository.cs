using Project2.Data.Model;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Data.Repository
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly Project2Context _context;

        public TreatmentRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Treatment> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
