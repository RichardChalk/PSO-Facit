using Microsoft.EntityFrameworkCore;
using SuperHeroAPIDemo_G_NET9.Models;

namespace SuperHeroAPIDemo_G_NET9.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }

}
