﻿using Microsoft.EntityFrameworkCore;
using Project2.Data.Model;
using Project2.Domain.Interface;
using Project2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Data.Repository
{
    public class NurseRepository : INurseRepository
    {
        private readonly Project2Context _context;

        public NurseRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateNurseAsync(Nurse nurse)
        {
            var nurseEntity = new NurseEntity
            {
                NurseId = nurse.NurseId,
                FirstName = nurse.FirstName,
                LastName = nurse.LastName,
            };

            _context.NurseEntity.Add(nurseEntity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteNurseAsync(Nurse nurse)
        {
            var Entity = _context.NurseEntity.Find(nurse.NurseId);

            _context.NurseEntity.Remove(Entity);

            await _context.SaveChangesAsync();
        }

        public async Task<Nurse> GetByNurseIdAsync(int nurseId)
        {
            var nurseEntity = await _context.NurseEntity.FindAsync(nurseId);
            if(nurseEntity == null)
            {
                return null;
            }
            return new Nurse(nurseEntity.NurseId, nurseEntity.FirstName, nurseEntity.LastName);
        }

        public async Task<IEnumerable<Nurse>> GetNursesAsync()
        {
            var Entities = await _context.NurseEntity.ToListAsync();

            return Entities.Select(e => new Nurse(e.NurseId, e.FirstName, e.LastName));
        }

        public async Task UpdateNurseAsync(Nurse nurse)
        {
            var entity = await _context.DoctorEntity.FindAsync(nurse.NurseId);
            var newEntity = new NurseEntity
            {
                NurseId = nurse.NurseId,
                FirstName = nurse.FirstName,
                LastName = nurse.LastName
            };

            _context.Entry(entity).CurrentValues.SetValues(newEntity);

            await _context.SaveChangesAsync();
        }
    }
}
