using BlazorBlog.WebApi.Data.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.WebApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<TagsTable> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


    }
}
