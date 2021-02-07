using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using CsvHelper;
using CsvHelper.Configuration;
using project.Models;

namespace project.Service
{
    public class CSVParser
    {
        public IEnumerable<Note> Parse(string filePath)
        {
            IEnumerable<Note> notes;
            if (!File.Exists(filePath))
            {
                return null;
            }

            Thread.Sleep(10);

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                csv.Context.RegisterClassMap<ReportMap>();
                notes = csv.GetRecords<Note>().ToList();
            }

            return notes;
        }

        public sealed class ReportMap : ClassMap<Note>
        {
            public ReportMap()
            {
                Map(m => m.Date).Name("Date");
                Map(m => m.ClientName).Name("ClientName");
                Map(m => m.ItemName).Name("ItemName");
                Map(m => m.Price).Name("Price");
            }
        }
    }
}
