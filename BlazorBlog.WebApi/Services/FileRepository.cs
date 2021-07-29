using BlazorBlog.WebApi.Contracts;
using BlazorBlog.WebApi.Data;
using BlazorBlog.WebApi.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBlog.WebApi.Services
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _db;

        public FileRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<int> Create(FileDetail entity)
        {
            EntityEntry<FileDetail> item = await _db.Files.AddAsync(entity);
            await Save();
            return item != null ? 1 : 0;
        }

        public async Task<bool> Delete(FileDetail entity)
        {
            _db.Files.Remove(entity);
            return await Save();
        }

        public async Task<IList<FileDetail>> FindAll()
        {
            List<FileDetail> files = await _db.Files.ToListAsync();
            return files;
        }

        public async Task<FileDetail> FindById(Guid id)
        {
            FileDetail file = await _db.Files.FirstOrDefaultAsync(x => x.Id == id);
            return file;
        }

        public async Task<bool> Save()
        {
            int changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(FileDetail entity)
        {
            _db.Files.Update(entity);
            return await Save();
        }
    }
}
