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
        private string AuthorLevel;
        private string Password;

        public string UserID1 { get => UserID; set => UserID = value; }
        public string UserName1 { get => UserName; set => UserName = value; }
        public string AuthorLevel1 { get => AuthorLevel; set => AuthorLevel = value; }
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

        internal static void AddData(string userID1, string userName1, string authorLevel1, string password1)
        {
            throw new NotImplementedException();
        }

        public static List<String> GetData()
        {
            List<String> entries = new List<string>();

            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT UserID, UserName, AuthorLevel, Password from MyLogin", db);
                SqliteDataReader query = selectCommand.ExecuteReader();

                int i = 0;
                while (query.Read())
                {
                    entries.Add(query.GetString(i));
                    i++;
                }
                db.Close();
            }
            return entries;
        }

    }
}
