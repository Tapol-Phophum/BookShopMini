using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopMiniApp
{
    class DataAccessUser
    {
        //Add Method for Create Table Data base
        private static string dbpath = "LoginTable.db"; //Create object: db for index path
        public static void InitializeLoginDatabase()
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                //Create Books table
                String LoginTableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyLogin (UserID NVARCHAR(13) PRIMARY KEY, " +
                    "UserName TEXT NOT NULL, " +
                    "AuthorLevel TEXT NOT NULL, " +
                    "Password TEXT NOT NULL)";
                SqliteCommand createLoginTable = new SqliteCommand(LoginTableCommand, db);
                createLoginTable.ExecuteReader();
            }
        }
    }
}
