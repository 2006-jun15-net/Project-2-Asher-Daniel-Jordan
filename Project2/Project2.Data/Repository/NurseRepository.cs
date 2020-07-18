using Project2.Data.Model;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2.Data.Repository
{
    public class NurseRepository : INurseRepository
    {
        private readonly Project2Context _context;

        public NurseRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Nurse> GetAll()
        {
            var Entities = _context.NurseEntity.ToList();

            return Entities.Select(e => new Nurse(e.NurseId, e.FirstName, e.LastName));
        }
    }
}
