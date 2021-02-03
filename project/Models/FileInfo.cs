using System;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class FileInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public Item Item { get; set; }
        public decimal Price
        {
            get { return Item.Price; }
            private set
            { }
        }
    }
}
