namespace ArticleLike.Context;

using ArticleLike.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Like> Likes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Like>().HasIndex(l => l.ArticleId).IsUnique();

        modelBuilder.Entity<Article>().HasData(
               new Article { Id = 1, Title = "Introduction to .NET", Content = "Learn the basics of .NET." },
               new Article { Id = 2, Title = "Getting Started with Redis", Content = "A guide on using Redis." }
           );

        modelBuilder.Entity<Like>().HasData(
           new Like { Id = 1, ArticleId = 1, LikeCount = 0 },
           new Like { Id = 2, ArticleId = 2, LikeCount = 0 }
       );
    }

}
