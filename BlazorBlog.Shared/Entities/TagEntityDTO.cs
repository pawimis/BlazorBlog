using System;
using System.Collections.Generic;

namespace BlazorBlog.Shared.Entities
{
    public class TagEntityDTO
    {
        public TagEntityDTO()
        {

        }
        public string TagText { get; set; }
        public virtual IEnumerable<BlogPostEntityDTO> Posts { get; set; }
        public DateTime CreateDate { get; set; }

    }
    public class TagEntityCreateDTO
    {
        public string TagText { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
