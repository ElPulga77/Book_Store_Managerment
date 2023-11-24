using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.Models.Models
{
    public class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Orders>();
        }
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
