using System.ComponentModel.DataAnnotations;

namespace Book_Store_Memoir.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; } 
        public string NameAuhor { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
