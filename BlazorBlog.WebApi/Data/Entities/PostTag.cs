
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BlazorBlog.WebApi.Data.Entities
{
    public partial class PostTag
    {

        [Required]
        [Key]
        public string TagText { get; set; }
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public virtual ICollection<BlogPost> Posts { get; set; }

    }
}
