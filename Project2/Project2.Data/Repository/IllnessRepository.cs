using Microsoft.EntityFrameworkCore;
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
    public class IllnessRepository : IIllnessRepository
    {
        private readonly Project2Context _context;

        public IllnessRepository(Project2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Illness> CreateAsync(Illness illness)
        {
            var illnessEntity = new IllnessEntity { IllnessId = illness.IllnessId, Name = illness.Name };

            _context.IllnessEntity.Add(illnessEntity);

            await _context.SaveChangesAsync();

            return illness;

        }

        public async Task<Illness> DeleteIllnessAsync(Illness illness)
        {
            var Entity = _context.IllnessEntity.Find(illness.IllnessId);

            

            _context.IllnessEntity.Remove(Entity);

            await _context.SaveChangesAsync();

            return illness;
        }

        public async Task<IEnumerable<Illness>> GetAllAsync()
        {
            var Entities = await _context.IllnessEntity.ToListAsync();

            return Entities.Select(e => new Illness(e.IllnessId, e.Name));
        }

        public async Task<Illness> GetByIdAsync(int id)
        {
            var illnessEntity = await _context.IllnessEntity.FindAsync(id);

            return (new Illness(illnessEntity.IllnessId, illnessEntity.Name));
        }

        public async Task UpdateIllnessAsync(Illness illness)
        {

            var entity = await _context.IllnessEntity.FindAsync(illness.IllnessId);
            var newEntity = new IllnessEntity
            {
                IllnessId = illness.IllnessId,
                Name = illness.Name,
                
   
            };

            _context.Entry(entity).CurrentValues.SetValues(newEntity);

            await _context.SaveChangesAsync();
        }
    }
}
