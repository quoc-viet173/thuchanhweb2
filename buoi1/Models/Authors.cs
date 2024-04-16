using System.ComponentModel.DataAnnotations;

namespace buoi1.Models
{
    public class Authors
    {
        [Key]
        public int AuthorID { get; set; }
        public string? FullName { get; set; }
        public List<Book_Author> Book_Authors { get; set; }
    }
}
