using Microsoft.Data.SqlClient;
using static PizzaData.Constants.TableNames;

namespace PizzaData.Helpers
{
    public class DatabaseHelper
    {
        /// <summary>
        /// Insert a list of objects into a database table using SqlBulkCopy.
        /// </summary>
        /// <param name="connection">An open SqlConnection to the database.</param>
        /// <param name="records">The list of records to insert.</param>
        /// <param name="tableName">The name of the table to insert the records into.</param>
        public static void InsertBulkData<T>(
            List<T> records, 
            SqlConnection connection,
            string tableName)
        {
            var orderDetailsTable = DataTableHelper.ConvertToDataTable(records);
            using (var bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.WriteToServer(orderDetailsTable);
            }
        }
    }
}
