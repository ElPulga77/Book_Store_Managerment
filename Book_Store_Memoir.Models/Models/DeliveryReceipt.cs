using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.Models.Models
{
    public class DeliveryReceipt
    {
        [Key]
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public int ShipperId { get; set; }
        [ForeignKey("ShipperId")]
        public Orders? Orders { get; set; } 
        public Shipper? Shipper { get; set; }
        public ICollection<ReceiptDetails>? ReceiptDetails { get; set; }

    }
}
