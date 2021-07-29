using BlazorBlog.WebApi.Data.Entities;

using System.Threading.Tasks;

namespace BlazorBlog.WebApi.Contracts
{
    public interface IPostRepository : IRepositoryBase<BlogPost>
    {
        Task<bool> Exists(int id);
        Task<BlogPost> FindById(int id);

    }
}
