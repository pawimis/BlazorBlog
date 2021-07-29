using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BlazorBlog.WebApi.Data.Entities
{
    public partial class BlogPost
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string GithubLink { get; set; }
        [Required]
        public string PostContent { get; set; }
        [Required]
        public string IntroPostContent { get; set; }
        public float BranchVersion { get; set; }
        public string FrontPostImage { get; set; }
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public virtual ICollection<PostTag> Tags { get; set; }
    }
}
