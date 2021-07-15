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
            CreateMap<TagsTable, TagEntityDTO>();
            CreateMap<TagEntityDTO, TagsTable>();

            CreateMap<BlogPost, BlogPostEntityDTO>().ForMember
                (dto => dto.Tags,
                opt =>
                opt.MapFrom(x => x.PostTagRelations.Select(y => y.Tag).ToList()));
            //CreateMap<PostTagRelation, TagEntityDTO>();
        }
    }
}
