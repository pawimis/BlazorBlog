using BlazorBlog.WebApi.Data.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBlog.WebApi.Contracts
{
    public interface ITagRepository : IRepositoryBase<PostTag>
    {
        Task<bool> Exists(string key);
        Task<PostTag> FindByKey(string key);

        Task<IEnumerable<PostTag>> CreateMany(IEnumerable<PostTag> entity);
    }
}
