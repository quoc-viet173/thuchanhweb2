using WebAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Domain
{
    public class Book_Author
    {
        [Key]
        public int ID { get; set; }

        public int BookId { get; set; }
        public Books Book { get; set; }

        public int AuthorId { get; set; }
        public Authors Author { get; set; }
    }
}
