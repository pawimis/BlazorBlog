using BlazorBlog.WebApi.Contracts;
using BlazorBlog.WebApi.Data;
using BlazorBlog.WebApi.Data.Entities;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBlog.WebApi.Services
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _db;

        public TagRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> Create(PostTag entity)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<PostTag> item = await _db.PostTags.AddAsync(entity);
            await Save();
            return 0;
        }

        public async Task<IEnumerable<PostTag>> CreateMany(IEnumerable<PostTag> entity)
        {
            await _db.PostTags.AddRangeAsync(entity);
            await Save();
            return entity;
        }

        public async Task<bool> Delete(PostTag entity)
        {
            _db.PostTags.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(string key)
        {
            return await _db.PostTags.AnyAsync(x => x.TagText == key);
        }

        public async Task<IList<PostTag>> FindAll()
        {
            return await _db.PostTags.ToListAsync();
        }

        public async Task<PostTag> FindByKey(string key)
        {
            return await _db.PostTags.FindAsync(key);
        }

        public async Task<bool> Save()
        {
            int changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(PostTag entity)
        {
            _db.PostTags.Update(entity);
            return await Save();
        }


    }
}
