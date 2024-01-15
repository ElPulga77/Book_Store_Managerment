using Book_Store_Memoir.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace Book_Store_Memoir.Models
{
    public class Author
    {
        public Author() { 
            BookAuthors = new HashSet<BookAuthor>();
        }
        
        [Key]
        public int? Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên tác giả!!!")]
        public string? NameAuhor { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập quê quán tác giả!!!")]
        public string? Hometown { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Death { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ảnh tác giả!!!")]
        public string? Image { get; set; }   
        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
