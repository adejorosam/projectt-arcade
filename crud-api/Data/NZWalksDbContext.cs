using System;
using Microsoft.EntityFrameworkCore;
using crud_api.Models.Domain;
namespace crud_api.Data
{
    public class NZWalksDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        //public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options): base(options)
        //{
        //}
        public NZWalksDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("NZWalks"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId);
        }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User_Role> Users_Roles { get; set; }


    }
}

