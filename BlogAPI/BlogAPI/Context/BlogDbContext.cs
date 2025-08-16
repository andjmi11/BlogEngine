using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Context
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options) { }
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Author> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTags>()
                .HasOne(bt => bt.BlogPost)
                .WithMany(bp => bp.Tags)
                .HasForeignKey(bt => bt.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
