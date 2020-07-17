using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookShopMiniApp
{
    class User
    {
        private string userID;
        private string userName;
        private string authorLevel;
        private string password;

        private static string dbpath = "LoginTable.db"; //Create object: db for index path

        public string UserID { get => userID; set => userID = value; }
        public string UserName { get => userName; set => userName = value; }
        public string AuthorLevel { get => authorLevel; set => authorLevel = value; }
        public string Password { get => password; set => password = value; }

        public User()
        {

        }
        public User(string userID, string userName, string authorLevel, string password)
        {
            UserID = userID;
            UserName = userName;
            AuthorLevel = authorLevel;
            Password = password;
        }

        public static void AddData(string UserID, string UserName, string AuthorLevel, string Password)
        {
            using (SqliteConnection db =
             new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks //ป้องกันการโจมตีฐานข้อมูล ข้อมูลต้องไม่เป็น SQL command
                insertCommand.CommandText = "INSERT INTO MyLogin VALUES ('" + UserID + "', @UserName, @Author, @Password);";
                insertCommand.Parameters.AddWithValue("@UserName", UserName);
                insertCommand.Parameters.AddWithValue("@Author", AuthorLevel);
                insertCommand.Parameters.AddWithValue("@Password", Password);
                insertCommand.ExecuteReader();

                db.Close();
            }
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
        public static List<String> GetData(string search)
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from MyLogin where UserID like " + "'" + search + "'", db);
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

        public static List<List<String>> SearchItem(string search)
        {
            List<List<String>> entries = new List<List<String>>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand searchCommand = new SqliteCommand
                ("SELECT * from MyLogin where UserID like " + "'%" + search + "%'" +
                                   " or UserName like" + "'%" + search + "%'"
                                   + " or Password like" + "'%" + search + "%'", db);
                SqliteDataReader query = searchCommand.ExecuteReader();

                while (query.Read())
                {
                    int i = 0;
                    List<string> searchResult = new List<string>();
                    while (i < query.FieldCount)
                    {
                        searchResult.Add(query.GetString(i));
                        i++;
                    }
                    entries.Add(searchResult);
                }                  
                db.Close();
            }
            return entries;
        }
        public static List<List<String>> SearchItem(string whereColoum,string searchExact)
        {
            List<List<String>> entries = new List<List<String>>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand searchCommand = new SqliteCommand
                ("SELECT * from MyLogin WHERE " + whereColoum + " like " + "'" + searchExact + "'", db);
                SqliteDataReader query = searchCommand.ExecuteReader();

                while (query.Read())
                {
                    int i = 0;
                    List<string> searchResult = new List<string>();
                    while (i < query.FieldCount)
                    {
                        searchResult.Add(query.GetString(i));
                        i++;
                    }
                    entries.Add(searchResult);
                }
                db.Close();
            }
            return entries;
        }
        public static void UpdateData(string UserID, string UserName, string AuthorLevel, string Password)
        {
            using (SqliteConnection db =
             new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand updateDataCommand = new SqliteCommand();
                updateDataCommand.Connection = db;
                updateDataCommand.CommandText = "UPDATE MyLogin SET UserName = @UserName, Author = @AuthorLevel, Password = @Password, Where UserID = @UserID;";
                updateDataCommand.Parameters.AddWithValue("@UserName", UserID);
                updateDataCommand.Parameters.AddWithValue("@Title", UserName);
                updateDataCommand.Parameters.AddWithValue("@Author", AuthorLevel);
                updateDataCommand.Parameters.AddWithValue("@Password", Password);
                updateDataCommand.ExecuteReader();
                db.Close();
            }

        }

        public static void DeleteData(string userID)
        {
            using (SqliteConnection db =
                new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                String deleteCommand = "delete from MyLogin where UserID = '" + userID + "' ";

                SqliteCommand deleteData = new SqliteCommand(deleteCommand, db);

                deleteData.ExecuteReader();

                db.Close();
            }
        }
    }
}
