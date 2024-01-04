using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.Models.Models
{
    [NotMapped]
    public class Checkout
    {
        public List<ShoppingCartVM> CartItems { get; set; } // Danh sách sản phẩm trong giỏ hàng
        [Required(ErrorMessage = "Vui lòng nhập tên người nhận hàng!!!")]
        public string Cusname { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!!!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email!!!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng!!!")]
        public string Address { get; set; }
        public decimal TotalAmount { get; set; } // Tổng tiền
        public string ShippingAddress { get; set; } // Địa chỉ giao hàng
        public string PaymentMethod { get; set; } // Phương thức thanh toán (ví dụ: Credit Card, COD, PayPal)
    }
}
