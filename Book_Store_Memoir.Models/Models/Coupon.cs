using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.Models.Models
{
    public class Coupon
    {
        public Coupon()
        {
            Orders = new HashSet<Orders>();
        }
        [Key]
        [Required(ErrorMessage = "Vui lòng nhập mã giảm giá!!!")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên chương trình giảm giá!!!")]
        public string CouponName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập phần trăm giảm giá!!!")]
        public int CouponPercentage { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày bắt đầu giảm giá!!!")]
        public DateTime StarDate { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày bắt đầu giảm giá!!!")]
        public DateTime EndDate { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }

    }
}
