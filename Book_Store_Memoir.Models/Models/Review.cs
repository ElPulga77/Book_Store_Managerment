using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store_Memoir.Models.Models
{
    public class Review
    {
        [Key]
        public int? Id { get; set; }
        public int? BookId { get; set; }
        [ForeignKey("BookId")]
        public Book? Book { get; set; }
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customers? Customers { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewDate { get; set; }
        
        
    }
}
