using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store_Memoir.Models
{
    public class Book
    {
        public int? Id { get; set; }
        public string? Title { get; set; }   
        public string? ISBN { get; set; }
        public DateTime? Publication_Date { get; set; }
        public int? PageNumber { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Image{ get; set; }
        public int? PublisherId { get; set; }
        [ForeignKey("Publisher_Id")]
        public int? LanguageId { get; set; }
        [ForeignKey("Language_Id")]
        public int? Category_Id { get; set; }
        [ForeignKey("Category_Id")]
        public virtual Category? Category { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public virtual Language? Language { get; set; }
        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
