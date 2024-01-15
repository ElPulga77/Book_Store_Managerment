using System.ComponentModel.DataAnnotations;

namespace Book_Store_Memoir.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên nhà xuất bản!!!")]
        public string Name { get; set; }
    }
}
