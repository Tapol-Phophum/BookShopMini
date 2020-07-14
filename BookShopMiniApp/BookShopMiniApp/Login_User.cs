using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopMiniApp
{
    class Login_User
    {
        private string UserID;
        private string UserName;
        private int AuthorLevel;
        private string Password;

        public string UserID1 { get => UserID; set => UserID = value; }
        public string UserName1 { get => UserName; set => UserName = value; }
        public int AuthorLevel1 { get => AuthorLevel; set => AuthorLevel = value; }
        public string Password1 { get => Password; set => Password = value; }

        private static string dbpath = "LoginTable.db"; //Create object: db for index path
        public static void AddData(string UserID1, string UserName1, int AuthorLevel1, string Password1)
        {
            using (SqliteConnection db =
             new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks //ป้องกันการโจมตีฐานข้อมูล ข้อมูลต้องไม่เป็น SQL command
                insertCommand.CommandText = "INSERT INTO MyLogin VALUES ('" + UserID1 + "', @UserName, @Author, @Password);";
                insertCommand.Parameters.AddWithValue("@UserName", UserName1);
                insertCommand.Parameters.AddWithValue("@Author", AuthorLevel1);
                insertCommand.Parameters.AddWithValue("@Password", Password1);
                insertCommand.ExecuteReader();

                db.Close();
            }
        }
    }
}
