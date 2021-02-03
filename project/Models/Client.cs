using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Client
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Budget { get; set; }
        [Required]
        public  int Id { get; set; }
    }
}
