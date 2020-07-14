using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopMiniApp
{
    class DataAccessBooks
    {
        //Add Method for Create Table Data base
        private static string dbpath = "BookShop.db"; //Create object: db for index path
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                //Create Books table
                String createBooksTableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyBooks (ISBN NVARCHAR(13) PRIMARY KEY, " +
                    "Publisher NVARCHAR(50) NOT NULL, " +
                    "Title NVACHAR(10) NOT NULL, " +
                    "Author NVARCHAR(50) NOT NULL, " +
                    "Addition NVARCHAR(200) NULL, " +
                    "Comment NVARCHAR(200) NULL, " +
                    "Price smallmoney NOT NULL, " +
                    "RecordDate DATE NULL, " +
                    "RecordBy NVARCHAR(100) NULL)";
                SqliteCommand createBooksTable = new SqliteCommand(createBooksTableCommand, db);
                createBooksTable.ExecuteReader();
                //Create Customers table
                String CustomersTableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyCustomers (Customer_Id NVARCHAR(10) PRIMARY KEY, " +
                    "Title VARCHAR(10) NOT NULL, " +
                    "First_Name NVARCHAR(50) NOT NULL, " +
                    "Last_Name NVARCHAR(50) NOT NULL, " +
                    "Address NVARCHAR(100) NOT NULL, " +
                    "District NVARCHAR(100) NOT NULL, " +
                    "City NVARCHAR(100) NOT NULL, " +
                    "Province NVARCHAR(100) NOT NULL, " +
                    "IdCard NVARCHAR(13) NOT NULL, " +
                    "Phone NVARCHAR(50) NOT NULL, " +
                    "Email NVARCHAR(80) NOT NULL UNIQUE," +
                    "RecordDate DATE NULL," +
                    "RecordBy NVARCHAR(100) NULL)";
                SqliteCommand createCustomersTable = new SqliteCommand(CustomersTableCommand, db);
                createCustomersTable.ExecuteReader();

                //Create Transactions table
                String OrderTransacTableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS OrderTransac (Order_Id INTEGER PRIMARY KEY, " +
                    "ISBN NVARCHAR(13) NOT NULL, " +
                    "Customer_Id NVARCHAR(10) NOT NULL, " +
                    "Quantity INTEGER NOT NULL, " +
                    "InputVAT money NOT NULL, " +
                    "Price money NOT NULL, " +
                    "Total_Price money NOT NULL, " +
                    "Date DATE NULL, " +
                    "RecordBy NVARCHAR(100) NULL)";
                SqliteCommand createOrderTransacTable = new SqliteCommand(OrderTransacTableCommand, db);
                createOrderTransacTable.ExecuteReader();
            }
        }
    }
}
