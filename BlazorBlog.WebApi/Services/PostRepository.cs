using BlazorBlog.WebApi.Contracts;
using BlazorBlog.WebApi.Data;
using BlazorBlog.WebApi.Data.Entities;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBlog.WebApi.Services
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _db;

        public PostRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(BlogPost entity)
        {
            await _db.Posts.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(BlogPost entity)
        {
            _db.Posts.Remove(entity);
            return await Save();
        }

        public async Task<IList<BlogPost>> FindAll()
        {
            List<BlogPost> post = await _db.Posts.ToListAsync();
            return post;
        }

        public async Task<BlogPost> FindById(int id)
        {
            BlogPost post = await _db.Posts.FindAsync(id);
            return post;
        }

        public async Task<bool> Save()
        {
            int changes = await _db.SaveChangesAsync();
            return changes > 0;

        }

        public async Task<bool> Update(BlogPost entity)
        {
            _db.Posts.Update(entity);
            return await Save();
        }
    }
}
