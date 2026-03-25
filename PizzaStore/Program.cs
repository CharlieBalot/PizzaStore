using Microsoft.Data.SqlClient;
using PizzaStore.Constants;
using PizzaStore.Services;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        DateTime currentTime = DateTime.Now;
        string formattedDateTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
        Console.WriteLine($"{formattedDateTime} Application started...");
        string[] filePaths = Directory.GetFiles(@"..\..\..\CSVFiles");
        var connection = new SqlConnection(@"Server=LAPTOP-H70BIPE0;Database=pizzaDB;Trusted_Connection=True;TrustServerCertificate=True;");
        try
        {
            // Check if files are existing in the CSV File folder
            foreach (string filePath in filePaths)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }

            connection.Open();
            foreach (string filePath in filePaths)
            {
                var fileName = Path.GetFileName(filePath);
                switch (fileName)
                {
                    case FileNames.OrderDetails:
                        Console.WriteLine("Processing Order Details data...");
                        OrderDetailsService orderDetailsService = new OrderDetailsService(new OrderDetailsService());
                        orderDetailsService.ProcessData(filePath, connection);
                        break;
                    case FileNames.Orders:
                        Console.WriteLine("Processing Orders data...");
                        OrderService ordersService = new OrderService(new OrderService());
                        ordersService.ProcessData(filePath, connection);
                        break;
                    case FileNames.Pizzas:
                        Console.WriteLine("Processing Pizzas data...");
                        PizzaService pizzaService = new PizzaService(new PizzaService());
                        pizzaService.ProcessData(filePath, connection);
                        break;
                    case FileNames.PizzaTypes:
                        Console.WriteLine("Processing Pizza type data...");
                        PizzaTypeService pizzaTypeService = new PizzaTypeService(new PizzaTypeService());
                        pizzaTypeService.ProcessData(filePath, connection);
                        break;
                    default:
                        Console.WriteLine("File Name not existing..");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            connection.Close();
            DateTime time = DateTime.Now;
            string CurrentDateTime = time.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"{CurrentDateTime} Application finished.");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
