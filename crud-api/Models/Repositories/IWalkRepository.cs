using System;
using crud_api.Models.Domain;

namespace crud_api.Models.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();

        Task<Walk> GetAsync(Guid id);

        Task<Walk> AddAsync(Walk walk);

        Task<Walk> DeleteAsync(Guid id);

        Task<Walk> UpdateAsync(Guid id, Walk walk);
    }
}

