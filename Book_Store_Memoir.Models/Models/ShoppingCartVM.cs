using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.Models.Models
{
    [Keyless]
    [NotMapped]
    public class ShoppingCartVM
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public double OrderTotal { get; set; }
    }
}
