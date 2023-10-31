using System.ComponentModel.DataAnnotations;

namespace Book_Store_Memoir.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Language_Name { get; set; }
    }
}
