using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Item
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NameItem { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
