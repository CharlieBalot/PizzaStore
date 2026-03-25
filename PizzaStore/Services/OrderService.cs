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
    public class OrderService : IFileRepository<Orders>
    {        
        private readonly OrderService _orderService;
        public OrderService(OrderService service)
        {
            _orderService = service;
        }

        public OrderService()
        {
        }

        public List<Orders> GetData(string filePath)
        {
            var records = FileHelper.GetCsvData<Orders>(filePath);
            return records;
        }

        public DataTable SetData(List<Orders> records)
        {
            var data = DataTableHelper.ConvertToDataTable(records);
            return data;
        }

        public void InsertData(List<Orders> records, SqlConnection connection)
        {
            try
            {
                DatabaseHelper.InsertBulkData(records, connection, TableName.Orders.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ProcessData(string filePath, SqlConnection connection)
        {
            var records = _orderService.GetData(filePath);
            var dataTable = _orderService.SetData(records);
            _orderService.InsertData(records, connection);
            Console.WriteLine("Orders data processed successfully.");
        }
    }
}