using System.Collections.Generic;
using System.IO;

namespace project.Models.Props
{
    public class Properties
    {
        private Dictionary<string, string> _properDictionary;
        private string _filepath = @"C:\Users\dpost\source\repos\project\project\Models\Props\properties.txt";

        public Properties(string filepath)
        {
            if (filepath != null)
            {
                _filepath = filepath;
            }

            _properDictionary = new Dictionary<string, string>();
            getPropertiesFromFile();
            
        }

        private Dictionary<string, string> getPropertiesFromFile()
        {
            string fileData = "";
            using (StreamReader sr = new StreamReader(_filepath))
            {
                fileData = sr.ReadToEnd().Replace("\r", "");
                fileData.Replace("\t", "");
                fileData.Replace(" ", "");
            }

            string[] values;
            string[] records = fileData.Split("\n".ToCharArray());
            foreach (var record in records)
            {
                values = record.Split("=".ToCharArray());
                _properDictionary.Add(values[0], values[1]);
            }

            return _properDictionary;
        }

        public Dictionary<string, string> GetProperties()
        {
            if (_properDictionary.Count != 0)
            {
                return _properDictionary;
            }

            return null;
        }

        public string GetPropertyValueByName(string propertyName)
        {
            var value = "";
            if (_properDictionary.TryGetValue(propertyName, out value))
            {
                return value;
            }

            return null;
        }
    }
}
