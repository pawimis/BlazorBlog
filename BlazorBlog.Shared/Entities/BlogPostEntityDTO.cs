using System;
using System.Collections.Generic;

namespace BlazorBlog.Shared.Entities
{
    public class BlogPostEntityDTO
    {
        public BlogPostEntityDTO(BlogPostEntityDTO post)
        {
            Id = post.Id;
            CreateDate = post.CreateDate;
            Title = post.Title;
            PostContent = post.PostContent;
            IntroPostContent = post.IntroPostContent;
            FrontPostImage = post.FrontPostImage;
            Tags = post.Tags;
            BranchVersion = post.BranchVersion;
        }
        public BlogPostEntityDTO()
        {

        }

        public string GithubLink { get; set; }
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public string IntroPostContent { get; set; }
        public string FrontPostImage { get; set; }
        public double BranchVersion { get; set; }

        public virtual IList<TagEntityDTO> Tags { get; set; }


    }
}
