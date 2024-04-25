using WebAPI.Models.Domain;

namespace WebAPI.Models.DTO
{
    public class AddBookRequestDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public  Publishers Publishers { get; set; }
        //navigation properties -
        public string PublisherID { get; set; }
        public List<int> AuthorIds { get; set; }
        public string? Description { get; set; }
    }
}
