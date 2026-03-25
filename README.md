# PizzaStore
Challenge project for guerilla360 application

### GOAL 
The goal of this test is to create a consumable RESTful API for importing, storing and retrieving sales 
data.

## Prerequisites
* Visual Studio with .Net Core installed
* Target Framework .Net 8.0
* MS SQL atleast v18.9.2
* PostMan/Bruno or other API development and testing
* NuGet Package Solution Installed
  - CsvHelper v33.1.0
  - Microsoft.AspNet.WebApi.Core v5.3.0
  - Microsoft.EntityFrameworkCore.Design v8.0.25
  - Microsoft.EntityFrameworkCore.SqlServer v8.0.25
  - Microsoft.EntityFrameworkCore.Tools v8.0.25


## How to Run
Note: Assuming that the CSV files are already in place in CSVFiles folder
1. Clone the repository to your local machine
2. Modify PizzaData Project Program.cs
   - Change the Connection String in SqlConnection  
3. Modify PizzaData/Models/PizzaDbContext.cs
   - In OnConfiguring methos, change the Connection String in UseSqlServer  
  
## How To Test
  ### INGESTING DATA FROM CSV FILE
  * Set PizzaData Project as Startup Project.
  * Run the project.
  * Once completed, go to your SQL
  * Perform below queries to check if data are ingested/uploaded in the database.
    ```sh
    SELECT * FROM [pizzaDB].[dbo].[PizzaTypes]
    SELECT * FROM [pizzaDB].[dbo].[Pizzas]
    SELECT * FROM [pizzaDB].[dbo].[OrderDetails]
    SELECT * FROM [pizzaDB].[dbo].[Orders]
    ```
    
    
  ### REST API
  * Set PizzaAPI Project as Startup Project
  * Go to any PostMan/Bruno or other API development and testing
  * Sample URL: 
      - Get Orders = https://localhost:PORT_NUMBER/orders?&pageNumber=1&pageSize=5
      - Get OrderDetails = https://localhost:PORT_NUMBER/order-details?&pageNumber=1&pageSize=5
      - Get Pizzas = https://localhost:PORT_NUMBER/pizza?&pageNumber=1&pageSize=5
      - Get Pizza Type = https://localhost:PORT_NUMBER/pizza-type?&pageNumber=1&pageSize=5
      - Get Statistics / Best Seller Pizza = https://localhost:PORT_NUMBER/statistics/get-best-seller-pizza
      - Get Statistics / Least Popular Pizza = https://localhost:PORT_NUMBER/statistics/get-best-seller-pizza

      
 
