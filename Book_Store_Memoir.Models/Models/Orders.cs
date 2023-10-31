using System;
using System.Collections.Generic;
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
        }
        public List<ShoppingCartVM> CartItems { get; set; }
        public int Id { get; set; } 
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book? Book { get; set; }
        public int? Quantity { get; set; }
        public string? CustomerId { get; set; }
        [ForeignKey("UserId")]
        public Customers Customers { get; set; }
    }
}
