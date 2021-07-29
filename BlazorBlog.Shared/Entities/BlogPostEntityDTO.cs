using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BlazorBlog.Shared.Entities
{
    public class BlogPostEntityDTO
    {
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
    public class BlogPostEntityCreateDTO
    {
        public int Id { get; set; }
        public string GithubLink { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PostContent { get; set; }
        [Required]
        public string IntroPostContent { get; set; }
        [Required]
        public string FrontPostImage { get; set; }
        public double BranchVersion { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public virtual IList<TagEntityCreateDTO> Tags { get; set; }

    }

}
