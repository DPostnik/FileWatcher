using CsvHelper.Configuration.Attributes;

namespace project.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Name("Date")]
        public string Date { get; set; }
        [Name("ClientName")]
        public string ClientName { get; set; }
        [Name("ItemName")]
        public string ItemName { get; set; }
        [Name("Price")]
        public decimal Price { get; set; }

        public Note(string date, string clientName, string itemName, decimal price)
        {
            Date = date;
            ClientName = clientName;
            ItemName = itemName;
            this.Price = price;
        }

        public Note()
        {
        }
    }
}
