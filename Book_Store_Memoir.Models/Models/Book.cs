using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Book_Store_Memoir.Models
{
    public class Book
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề sách!!!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã tiêu chuẩn!!!")]
        public string? ISBN { get; set; }
        public DateTime? Publication_Date { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số trang!!!")]
        public int? PageNumber { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mô tả sản phẩm!!!")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá tiền!!!")]
        public double? Price { get; set; }
       
        public string? Image{ get; set; }
        [Required(ErrorMessage = "Vui lòng chọn nhà xuất bản!!!")]
        public int? PublisherId { get; set; }
        [ForeignKey("Publisher_Id")]
        [Required(ErrorMessage = "Vui lòng chọn ngôn ngữ!!!")]
        public int? LanguageId { get; set; }
        [ForeignKey("Language_Id")]
        [Required(ErrorMessage = "Vui lòng chọn thể loại!!!")]
        public int? Category_Id { get; set; }
        [ForeignKey("Category_Id")]
        public virtual Category? Category { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public virtual Language? Language { get; set; }
        [AllowNull]
        public ICollection<BookAuthor>? BookAuthors { get; set; }
        [AllowNull]
        public ICollection<ReceiptDetails>? ReceiptDetails { get; set; }
    }
}
