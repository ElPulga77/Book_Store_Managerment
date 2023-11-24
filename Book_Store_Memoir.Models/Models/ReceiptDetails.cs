using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.Models.Models
{
    public class ReceiptDetails
    {
        public int? DeliveryReceiptId { get; set; }
        public int? BookId { get; set; } 
        public Book? Book { get; set; }
        public DeliveryReceipt? DeliveryReceipt { get; set; }
    }
}
