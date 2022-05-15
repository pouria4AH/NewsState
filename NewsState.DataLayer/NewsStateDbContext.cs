using Microsoft.EntityFrameworkCore;
using NewsState.DataLayer.Entities;

namespace NewsState.DataLayer
{
    public class NewsStateDbContext : DbContext
    {
        public NewsStateDbContext(DbContextOptions<NewsStateDbContext> options) : base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
