using System;
using System.Collections.Generic;

namespace BlazorBlog.Shared.Entities
{
    public class BlogPost
    {
        public BlogPost(BlogPost post)
        {
            Id = post.Id;
            CreateDate = post.CreateDate;
            Title = post.Title;
            Content = post.Content;
            IntroContent = post.IntroContent;
            FrontPostImage = post.FrontPostImage;
            Tags = post.Tags;
            BranchVersion = post.BranchVersion;
        }
        public BlogPost()
        {

        }

        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string IntroContent { get; set; }
        public string FrontPostImage { get; set; }
        public List<string> Tags { get; set; }
        public double BranchVersion { get; set; }

    }
}
