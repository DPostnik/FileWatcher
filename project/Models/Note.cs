using System;

namespace project.Models
{
    public class Note
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ClientName { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }

        public Note(DateTime date, string clientName, string itemName, decimal price)
        {
            Date = date;
            ClientName = clientName;
            ItemName = itemName;
            this.Price = price;
        }
    }
}
