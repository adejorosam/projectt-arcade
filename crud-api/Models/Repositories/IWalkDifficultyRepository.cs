using System;
using crud_api.Models.Domain;

namespace crud_api.Models.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();

        Task<WalkDifficulty> GetAsync(Guid id);

        Task<WalkDifficulty> AddAsync(WalkDifficulty WalkDifficulty);

        Task<WalkDifficulty> DeleteAsync(Guid id);

        Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty WalkDifficulty);
    }
}

