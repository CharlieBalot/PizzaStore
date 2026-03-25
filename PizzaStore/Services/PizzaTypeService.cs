using Microsoft.Data.SqlClient;
using PizzaData.Helpers;
using PizzaData.Models;
using PizzaStore.Constants;
using PizzaStore.Helpers;
using PizzaStore.Interfaces;
using System.Data;
using System.Text;
using static PizzaData.Constants.TableNames;

namespace PizzaStore.Services
{
    public class PizzaTypeService : IFileRepository<PizzaType>
    {
        public string FileName => FileNames.PizzaTypes;
        private readonly PizzaTypeService _pizzaTypeService;
        public PizzaTypeService(PizzaTypeService service)
        {
            _pizzaTypeService = service;
        }
        public PizzaTypeService()
        {
        }

        public List<PizzaType> GetData(string filePath)
        {
            var records = FileHelper.GetCsvData<PizzaType>(filePath);
            return records;
        }

        public DataTable SetData(List<PizzaType> records)
        {
            var data = DataTableHelper.ConvertToDataTable(records);
            return data;
        }

        public void InsertData(List<PizzaType> records, SqlConnection connection)
        {
            try
            {
                DatabaseHelper.InsertBulkData(records, connection, TableName.PizzaTypes.ToString());
            }
            catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ProcessData(string filePath, SqlConnection connection)
        {            
            var records = _pizzaTypeService.GetData(filePath);
            var dataTable = _pizzaTypeService.SetData(records);
            _pizzaTypeService.InsertData(records, connection);
            Console.WriteLine("Pizza Type data processed successfully.");
        }
    }
}
