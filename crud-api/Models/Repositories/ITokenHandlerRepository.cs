using System;
using crud_api.Models.Domain;

namespace crud_api.Models.Repositories
{
    public interface ITokenHandlerRepository
    {
        Task<string> CreateTokenAsync(User user);
    }
}

