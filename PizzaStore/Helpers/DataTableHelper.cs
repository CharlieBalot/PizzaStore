using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PizzaData.Helpers
{
    public class DataTableHelper
    {
        /// <summary>
        /// Convert a list of objects to a DataTable
        /// Returns a DataTable with columns corresponding to the properties 
        /// of the objects and rows corresponding to the values of those 
        /// properties for each object in the list.
        /// </summary>
        /// <param name="items">List of objects to convert</param>
        public static DataTable ConvertToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            // Get all the properties of the model
            PropertyInfo[] properties = typeof(T).GetProperties();

            // Create the DataTable columns
            foreach (PropertyInfo prop in properties)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Populate the DataTable rows
            foreach (T item in items)
            {
                var values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
