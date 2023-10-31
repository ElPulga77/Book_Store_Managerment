using System.ComponentModel.DataAnnotations;

namespace Book_Store_Memoir.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
    }
}
