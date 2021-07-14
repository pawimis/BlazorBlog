using BlazorBlog.Shared.Entities;

using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> Posts { get; set; }
    }
}
