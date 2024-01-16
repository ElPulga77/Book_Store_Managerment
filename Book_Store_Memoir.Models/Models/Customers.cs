using System.ComponentModel.DataAnnotations;

namespace Book_Store_Memoir.Models.Models
{
    public class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên người dùng!!!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu!!!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại!!!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ!!!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email!!!")]
        public string Email { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
