namespace BlazorBlog.Shared.Entities
{
    public class TagEntityDTO
    {
        public TagEntityDTO(int id, string tagText)
        {
            Id = id;
            TagText = tagText;
        }

        public int Id { get; set; }
        public string TagText { get; set; }
    }
}
