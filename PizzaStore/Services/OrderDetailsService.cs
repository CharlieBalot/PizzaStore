using Microsoft.Data.SqlClient;
using PizzaData.Helpers;
using PizzaData.Models;
using PizzaStore.Constants;
using PizzaStore.Helpers;
using PizzaStore.Interfaces;
using System.Data;
using static PizzaData.Constants.TableNames;

namespace PizzaStore.Services
{
    public class OrderDetailsService : IFileRepository<OrderDetails>
    {
        public string FileName => FileNames.OrderDetails;
        private readonly OrderDetailsService _orderDetailsService;
        public OrderDetailsService(OrderDetailsService service)
        {
            _orderDetailsService = service;
        }

        public OrderDetailsService()
        {
        }

        public List<OrderDetails> GetData(string filePath)
        {
            var records = FileHelper.GetCsvData<OrderDetails>(filePath);
            return records;
        }

        public DataTable SetData(List<OrderDetails> records)
        {
            var data = DataTableHelper.ConvertToDataTable(records);
            return data;
        }

        public void InsertData(List<OrderDetails> records, SqlConnection connection)
        {
            try
            {
                DatabaseHelper.InsertBulkData(records, connection, TableName.OrderDetails.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ProcessData(string filePath, SqlConnection connection)
        {
            var records = _orderDetailsService.GetData(filePath);
            var dataTable = _orderDetailsService.SetData(records);
            _orderDetailsService.InsertData(records, connection);
            Console.WriteLine("Order Details data processed successfully.");
        }
    }
}
