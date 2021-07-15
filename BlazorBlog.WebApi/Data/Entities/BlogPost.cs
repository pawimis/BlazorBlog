using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorBlog.WebApi.Data.Entities
{
    public partial class BlogPost
    {
        public BlogPost()
        {
            PostTagRelations = new HashSet<PostTagRelation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string GithubLink { get; set; }
        public string PostContent { get; set; }
        public string IntroPostContent { get; set; }
        public decimal? BranchVersion { get; set; }
        public string FrontPostImage { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<PostTagRelation> PostTagRelations { get; set; }
    }
}
