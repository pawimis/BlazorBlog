using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorBlog.WebApi.Data.Entities
{
    public partial class PostTagRelation
    {
        public int TagId { get; set; }
        public int PostId { get; set; }

        public virtual BlogPost Post { get; set; }
        public virtual TagsTable Tag { get; set; }
    }
}
