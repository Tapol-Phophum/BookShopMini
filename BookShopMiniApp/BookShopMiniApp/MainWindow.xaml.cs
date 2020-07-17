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
        private static string dbpath = "LoginTable.db"; //Create object: db for index path
        public MainWindow()
        {
            InitializeComponent();
            DataAccessBooks.InitializeDatabase();
            DataAccessUser.InitializeLoginDatabase();
            txtUserName.Focus();
        }
        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            string result;
            if (String.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Please input 'UserID' for delete");
            }
            else if (string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Please input Password");
            }
            else
            {
                using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    SqliteCommand selectCommand = new SqliteCommand
                   ("SELECT COUNT (*) FROM MyLogin", db);
                    SqliteDataReader query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        result = query.GetString(0);
                        if (result == "0")
                        {
                            UserRegister userRegister = new UserRegister();
                            userRegister.Show();
                        }
                        else
                        {
                            string userName = txtUserName.Text;
                            string passWord = txtPassword.Password;
                            string sql = "SELECT * FROM MyLogin WHERE UserName ='" + userName + "' AND Password ='" + passWord + "' AND AuthorLevel ='5'";
                            using (SqliteConnection db1 =
                                   new SqliteConnection($"Filename={dbpath}"))
                            {
                                db1.Open();
                                SqliteCommand selectCommand1 = new SqliteCommand(sql, db1);
                                SqliteDataReader query1 = selectCommand1.ExecuteReader();
                                while (query1.Read())
                                {
                                    this.Hide();
                                    UserRegister userRegister = new UserRegister();
                                    userRegister.Show();
                                    return;
                                }
                                db1.Close();
                            }
                        }
                        //MessageBox.Show(result.ToString());
                    }
                    db.Close();
                }
            }
        }
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Please input Username");
            }
            else if(string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Please input Password");
            }
            else
            {
                string userName = txtUserName.Text;
                string passWord = txtPassword.Password;
                string sql = "SELECT* from MyLogin WHERE UserName ='" + userName + "'AND Password ='" + passWord + "'";
                using (SqliteConnection db =
                       new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    SqliteCommand selectCommand = new SqliteCommand(sql, db);
                    SqliteDataReader query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        this.Hide();
                        Welcome welcome = new Welcome(userName);
                        welcome.Show();
                        return;
                    }
                    db.Close();
                }
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void chbPasswordShow_Checked(object sender, RoutedEventArgs e)
        {
            txtPassword.PasswordChar = default(char);
        }
        private void ShowPasswordFunction()
        {
            ShowPassword.Text = "HIDE";
            txtPasswordUnmask.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Hidden;
            txtPasswordUnmask.Text = txtPassword.Password;
        }

        private void HidePasswordFunction()
        {
            ShowPassword.Text = "SHOW";
            txtPasswordUnmask.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Visible;
        }

        private void ShowPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            HidePasswordFunction();
        }

        private void ShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPasswordFunction();
        }

        private void ShowPassword_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            HidePasswordFunction();
        }

        private void txtUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = "";
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Password = "";
            txtPasswordUnmask.Text = "";
        }
    }
}
