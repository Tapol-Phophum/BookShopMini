using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookShopMiniApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataAccessBooks.InitializeDatabase();
            DataAccessUser.InitializeLoginDatabase();
            txtUserName.Focus();
        }
        private static string dbpath = "LoginTable.db";

        private void txtUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = "";
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Text = "";
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((txtUserName.Text == "Admin") || (txtUserName.Text == "admin"))
            {
                if (txtPassword.Text == "admin")
                {
                    UserRegister userRegister = new UserRegister();
                    userRegister.Show();
                }
                else
                {
                    MessageBox.Show("โปรดติดต่อผู้ขายขอ Password ที่ถูกต้อง");
                }
            }
            else
            {
                MessageBox.Show("โปรดติดต่อผู้ขายขอ UserName ที่ถูกต้อง");
            }
        }

        private void loginBtn_Click_1(object sender, RoutedEventArgs e)
        {
             string sql = "SELECT* from MyLogin WHERE UserName ='" + txtUserName.Text + "'AND Password ='" + txtPassword.Text + "'";
            using (SqliteConnection db =
                   new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand(sql, db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    Welcome welcome = new Welcome(txtUserName.Text);
                    welcome.Show();
                    return;
                }
                db.Close();
            }
        }
    }
}
