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
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
