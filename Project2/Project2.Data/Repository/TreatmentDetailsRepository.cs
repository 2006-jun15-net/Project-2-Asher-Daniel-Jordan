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
    public class TreatmentDetailsRepository : ITreatmentDetailsRepository
    {
        private readonly Project2Context _context;

        public TreatmentDetailsRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(TreatmentDetails td)
        {
            var tdEntity = new TreatmentDetailsEntity
            {
                StartTime = DateTime.Now.ToString(),
                PatientId = td.PatientId,
                OpsRoomId = td.OpsRoomId,
                TreatmentId = td.TreatmentId
            };

            _context.TreatmentDetailsEntity.Add(tdEntity);

            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(TreatmentDetails td)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TreatmentDetails>> GetAllAsync()
        {
            var Entities = await _context.TreatmentDetailsEntity.ToListAsync();

            return Entities.Select(e => new TreatmentDetails(e.TreatmentDetailsId, e.OpsRoomId, e.PatientId, e.TreatmentId, e.StartTime));
        }

        public async Task<TreatmentDetails> GetByIdAsync(int id)
        {
            var entity = await _context.TreatmentDetailsEntity.FindAsync(id);

            return new TreatmentDetails(
                entity.TreatmentDetailsId,
                entity.OpsRoomId, 
                entity.PatientId, 
                entity.TreatmentId, 
                entity.StartTime
                );
        }

        public async Task<TreatmentDetails> GetPatientTreatment(int id)
        {
            var entities = await _context.TreatmentDetailsEntity.Where(e => e.PatientId == id).ToListAsync();
            DateTime maxDate = DateTime.MinValue;
            foreach(var e in entities)
            {
                if(DateTime.Parse(e.StartTime) > maxDate)
                {
                    maxDate = DateTime.Parse(e.StartTime);
                }
            }
            var entity = await _context.TreatmentDetailsEntity
                .FirstOrDefaultAsync(e => e.StartTime == maxDate.ToString());

            return new TreatmentDetails(
                entity.TreatmentDetailsId, 
                entity.OpsRoomId, 
                entity.PatientId, 
                entity.TreatmentId, 
                entity.StartTime
                );
        }

        public async Task UpdateAsync(TreatmentDetails td)
        {
            var currentDetail = await _context.TreatmentDetailsEntity.FindAsync(td.TreatmentDetailsId);
            var entity = new TreatmentDetailsEntity
            {
                TreatmentDetailsId = td.TreatmentDetailsId,
                PatientId = td.PatientId,
                TreatmentId = td.TreatmentId,
                OpsRoomId = td.OpsRoomId,
                StartTime = td.StartTime
            };

            _context.Entry(currentDetail).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
        }
    }
}

