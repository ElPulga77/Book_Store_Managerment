using System.ComponentModel.DataAnnotations;

namespace Book_Store_Memoir.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên thể loại!!!")]
        public string Name { get; set; }

    }
}
