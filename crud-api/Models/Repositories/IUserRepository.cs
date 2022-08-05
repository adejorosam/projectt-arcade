using System;
using crud_api.Models.Domain;

namespace crud_api.Models.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}

