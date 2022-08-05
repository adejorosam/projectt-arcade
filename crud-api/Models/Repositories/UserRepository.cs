using System;
using crud_api.Data;
using crud_api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace crud_api.Models.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly NZWalksDbContext nzWalksDbContext;



        public UserRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nzWalksDbContext = nZWalksDbContext;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await nzWalksDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username && x.Password == password);

            if(user == null)
            {
                return null;
            }

            var userRoles = await nzWalksDbContext.User_Roles.Where(x => x.UserId == user.Id).ToListAsync();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await nzWalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.Id);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
    }
}

