using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorBlog.WebApi.Data.Entities
{
    public partial class TagsTable
    {
        public TagsTable()
        {
            PostTagRelations = new HashSet<PostTagRelation>();
        }

        public int Id { get; set; }
        public string TagText { get; set; }

        public virtual ICollection<PostTagRelation> PostTagRelations { get; set; }
    }
}
