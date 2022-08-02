using System;
using Microsoft.EntityFrameworkCore;
using crud_api.Data;
using crud_api.Models.Domain;
namespace crud_api.Models.Repositories
{
    public class WalkDifficultyRepository: IWalkDifficultyRepository
    {

        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;

        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            var walkDifficulty = await nZWalksDbContext.WalkDifficulty.ToListAsync();

            return walkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            //existingWalkDifficulty.Gu = walkDifficulty.Length;
            existingWalkDifficulty.Code = walkDifficulty.Code;
            

            await nZWalksDbContext.SaveChangesAsync();

            return existingWalkDifficulty;

        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;

        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            var walkDifficulty = await
                nZWalksDbContext.WalkDifficulty
                //.Include(x => x.Region)
                //.Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDifficulty = await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            if (walkDifficulty == null)
            {
                return null;
            }

            //Delete the Walk
            nZWalksDbContext.WalkDifficulty.Remove(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;

        }
    }
}

