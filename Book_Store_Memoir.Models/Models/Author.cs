using System.ComponentModel.DataAnnotations;

namespace Book_Store_Memoir.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; } 
        public string? NameAuhor { get; set; }
        public string? Hometown { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Death { get; set; }
        public string? Image { get; set; }   
        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
