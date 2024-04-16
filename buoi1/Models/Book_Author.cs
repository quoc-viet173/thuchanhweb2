using System.ComponentModel.DataAnnotations;

namespace buoi1.Models
{
    public class Book_Author
    {
        [Key]
        public int ID { get; set; }

        public int BookID { get; set; }
        public Books Book { get; set; }

        public int AuthorID { get; set; }
        public Authors Author { get; set; }
    }
}
