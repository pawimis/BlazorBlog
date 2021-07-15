using System.Collections.Generic;

#nullable disable

namespace BlazorBlog.WebApi.Data.Entities
{
    public partial class TagsTable
    {
        public int Id { get; set; }
        public string TagText { get; set; }

        public virtual IList<BlogPost> Tags { get; set; }

    }
}
