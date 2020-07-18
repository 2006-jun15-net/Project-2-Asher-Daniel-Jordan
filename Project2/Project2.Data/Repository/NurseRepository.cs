using Project2.Data.Model;
using Project2.Domain.Interface;
using System;
using System.Collections.Generic;
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
    }
}
