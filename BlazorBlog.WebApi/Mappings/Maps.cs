using AutoMapper;

using BlazorBlog.Shared.Entities;
using BlazorBlog.WebApi.Data.Entities;

using System.Linq;

namespace BlazorBlog.WebApi.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<TagsTable, TagEntityDTO>().ReverseMap();

            CreateMap<BlogPost, BlogPostEntityDTO>().ForMember(dto => dto.Tags,
                opt => opt.MapFrom(x => x.Tags.Select(y => y.Tags).ToList()));

        }
    }
}
