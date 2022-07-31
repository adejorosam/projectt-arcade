using System;
using Microsoft.EntityFrameworkCore;
using crud_api.Models.Domain;
namespace crud_api.Data
{
    public class NZWalksDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

    public NZWalksDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server with connection string from app settings
        options.UseSqlServer(Configuration.GetConnectionString("NZWalks"));
    }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

    } 
}

