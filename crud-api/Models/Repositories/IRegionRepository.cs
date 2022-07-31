﻿using System;
using crud_api.Models.Domain;

namespace crud_api.Models.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetAsync(Guid id);

        Task<Region> AddAsync(Region region);


    }
}

