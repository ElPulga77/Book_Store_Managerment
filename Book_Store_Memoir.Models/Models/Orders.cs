using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.Models.Models
{
    public partial class Orders
    {

        public Orders()
        {
            CartItems = new List<ShoppingCartVM>();
            DeliveryReceipts = new HashSet<DeliveryReceipt>();
        }
        public List<ShoppingCartVM> CartItems { get; set; }
        public int Id { get; set; } 
        public int? BookId { get; set; }
        [ForeignKey("BookId")]
        public Book? Book { get; set; }
        public double? TotalAmount { get; set; }
        public int? Quantity { get; set; }
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public int? ShipperId { get; set; }
        [ForeignKey("ShipperId")]
        public int? OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public string? CouponId { get; set; }
        [ForeignKey("CouponId")]
        public string? RecieverName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OrderDate { get; set; }
        public Customers? Customers { get; set; }
        public Shipper? Shipper { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public Coupon? Coupon { get; set; }
        public int? CustomersCustomerId { get; set; }
        public virtual ICollection<DeliveryReceipt> DeliveryReceipts { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
