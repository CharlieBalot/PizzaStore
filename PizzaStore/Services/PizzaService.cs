using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaData.Helpers;
using PizzaData.Models;
using PizzaStore.Constants;
using PizzaStore.Helpers;
using PizzaStore.Interfaces;
using System.Data;
using static PizzaData.Constants.TableNames;

namespace PizzaStore.Services
{
    public class PizzaService : IFileRepository<Pizzas>
    {
        public string FileName => FileNames.Pizzas;
        private readonly PizzaService _pizzaService;
        public PizzaService(PizzaService service)
        {
            _pizzaService = service;
        }
        public PizzaService()
        {
        }

        public List<Pizzas> GetData(string filePath)
        {
            var records = FileHelper.GetCsvData<Pizzas>(filePath);
            return records;
        }

        public DataTable SetData(List<Pizzas> records)
        {
            var data = DataTableHelper.ConvertToDataTable(records);
            return data;
        }

        public void InsertData(List<Pizzas> records, SqlConnection connection)
        {
            try
            {
                DatabaseHelper.InsertBulkData(records, connection, TableName.Pizzas.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ProcessData(string filePath, SqlConnection connection)
        {
            var records = _pizzaService.GetData(filePath);
            var dataTable = _pizzaService.SetData(records);
            _pizzaService.InsertData(records, connection);
            Console.WriteLine("Pizza data processed successfully.");
        }
    }    
}
