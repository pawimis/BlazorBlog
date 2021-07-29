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
            CreateMap<PostTag, TagEntityCreateDTO>().ReverseMap();
            CreateMap<PostTag, TagEntityDTO>().ReverseMap();
            CreateMap<PostTag, TagEntityDTO>().ForMember
             (dto => dto.Posts,
             opt =>
             opt.MapFrom(x => x.Posts.Select(y => y).ToList())).ReverseMap();
            CreateMap<BlogPost, BlogPostEntityDTO>().ReverseMap();
            CreateMap<BlogPost, BlogPostEntityCreateDTO>().ReverseMap();
            CreateMap<BlogPost, BlogPostEntityDTO>().ForMember
                (dto => dto.Tags,
                opt =>
                opt.MapFrom(x => x.Tags.Select(y => y).ToList())).ReverseMap();

            //CreateMap<PostTagRelation, TagEntityDTO>();
        }
    }
}
