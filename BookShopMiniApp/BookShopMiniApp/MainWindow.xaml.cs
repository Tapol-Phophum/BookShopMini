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

        //private void txtUserName_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    txtUserName.Text = "";
        //}

        //private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    txtPassword.Password = "";
        //}

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((txtUserName.Text == "Admin") || (txtUserName.Text == "admin"))
            {
                if (txtPassword.Password == "admin")
                {

                    UserRegister userRegister = new UserRegister();
                    this.Close();
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
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            Login_User.CheckAuthorLogin(txtUserName.Text, txtPassword.Password);
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
    }
}
