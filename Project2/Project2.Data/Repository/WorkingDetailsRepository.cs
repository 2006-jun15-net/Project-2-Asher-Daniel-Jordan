using Project2.Data.Model;
using Project2.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Data.Repository
{
    public class WorkingDetailsRepository : IWorkingDetailsRepository
    {
        private readonly Project2Context _context;

        public WorkingDetailsRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
