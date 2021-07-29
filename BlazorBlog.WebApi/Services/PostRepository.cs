using BlazorBlog.WebApi.Contracts;
using BlazorBlog.WebApi.Data;
using BlazorBlog.WebApi.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> Create(BlogPost entity)
        {
            if (entity.Tags != null && entity.Tags.Any())
            {
                foreach (PostTag tag in entity.Tags)
                {
                    if (await _db.PostTags.AnyAsync(x => x.TagText == tag.TagText))
                    {
                        _db.PostTags.Attach(tag);
                    }
                }
            }
            EntityEntry<BlogPost> item = await _db.BlogPosts.AddAsync(entity);
            await Save();
            return item.Entity.Id;
        }

        public async Task<bool> Delete(BlogPost entity)
        {
            _db.BlogPosts.Remove(entity);
            return await Save();
        }

        public Task<bool> Exists(int id)
        {
            return _db.BlogPosts.AnyAsync(x => x.Id == id);
        }

        public async Task<IList<BlogPost>> FindAll()
        {
            List<BlogPost> post = await _db.BlogPosts.Include(x => x.Tags).ToListAsync();
            return post;
        }

        public async Task<BlogPost> FindById(int id)
        {
            BlogPost post = await _db.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            return post;
        }

        public async Task<bool> Save()
        {
            int changes = await _db.SaveChangesAsync();
            return changes > 0;

        }

        public async Task<bool> Update(BlogPost entity)
        {
            _db.BlogPosts.Update(entity);
            return await Save();
        }
    }
}
