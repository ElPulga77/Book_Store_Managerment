using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.Models.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int? BookId { get; set; }
        [ForeignKey("Book_Id")]
        public int? OrdersId { get; set; }
        [ForeignKey("Order_Id")]
        public double? TotalAmount { get; set; }
        public int? Quantity { get; set; }   
        public Orders? Orders { get; set; }
        public Book? Book { get; set; }


    }
}
