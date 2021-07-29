using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorBlog.WebApi.Data.Entities
{
    public class FileDetail
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string DocumentName { get; set; }
        public string DocId { get; set; }
        public string DocType { get; set; }
        public string DocUrl { get; set; }
    }
}
