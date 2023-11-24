
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.Models.Models
{
    public class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Orders>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }    
        public string BirthDay { get; set; }
        public string ImageAvt { get; set; }
        public string CCCD { get; set; }
        public string Adress { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<DeliveryReceipt> DeliveryReceipts { get; set; }
    }
}
