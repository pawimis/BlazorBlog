using BlazorBlog.WebApi.Data.Entities;

using System;
using System.Threading.Tasks;

namespace BlazorBlog.WebApi.Contracts
{
    public interface IFileRepository : IRepositoryBase<FileDetail>
    {
        Task<FileDetail> FindById(Guid id);

    }
}
