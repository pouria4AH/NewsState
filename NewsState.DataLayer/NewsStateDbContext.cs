using Microsoft.EntityFrameworkCore;

namespace NewsState.DataLayer
{
    public class NewsStateDbContext : DbContext
    {
        public NewsStateDbContext(DbContextOptions<NewsStateDbContext> options) : base(options)
        {
            
        }

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
