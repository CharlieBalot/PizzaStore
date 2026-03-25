using CsvHelper;
using CsvHelper.Configuration;
using PizzaData.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaStore.Helpers
{
    public class FileHelper
    {

        /// <summary>
        /// Read CSV file
        /// Returns a list of records of type T
        /// </summary>
        /// <param name="filePath">The path of the file to check</param>
        public static List<T> GetCsvData<T>(string filePath) where T : class, new()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, 
                MissingFieldFound = null 
            };

            List<T> records = new List<T>();
            if (CheckIfFileExists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    using (var csv = new CsvReader(reader, config))
                    {
                        records = csv.GetRecords<T>().ToList();
                    }
                }
            }
            return records;
        }
        /// <summary>
        /// Check if file exists
        /// Returns true if the file exists, false otherwise
        /// </summary>
        /// <param name="filePath">The path of the file to check</param>
        public static bool CheckIfFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                throw new ArgumentException("File not found.");
            }
            return true;
        }
    }
}
