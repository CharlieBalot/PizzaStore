using Microsoft.Data.SqlClient;
using PizzaData.Models;
using PizzaStore.Services;
using System.Data;

namespace PizzaStore.Interfaces
{
    public interface IFileRepository<T> where T : class
    {
        // Get Data from CSV file and return a list of T
        List<T> GetData(string filePath);
        // Set Data from a list of T and return a DataTable
        DataTable SetData(List<T> records);
        // Insert Data from a list of T into the database using a SqlConnection
        void InsertData(List<T> records, SqlConnection connection);
        // Process Data from a CSV file and insert it into the database using a SqlConnection
        void ProcessData(string filePath, SqlConnection connection);
    }
}
