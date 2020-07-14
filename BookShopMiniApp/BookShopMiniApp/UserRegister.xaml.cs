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
using System.Windows.Shapes;

namespace BookShopMiniApp
{
    /// <summary>
    /// Interaction logic for UserRegister.xaml
    /// </summary>
    public partial class UserRegister : Window
    {
        string lblContent = "";
        public UserRegister()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Login_User login_User = new Login_User();
            if (CheckWhetherBox())
            {
                MessageBox.Show("Please Input data in :" + lblContent);
                lblContent = "";
                return;
            }
            else

                login_User.UserID1 = txtUserID.Text;
            login_User.UserName1 = txtUsername.Text;
            login_User.AuthorLevel1 = int.Parse(cboLevelAuthor.Text);
            login_User.Password1 = txtPassword.Password;
            // ทำการบันทึกข้อมูล
            Login_User.AddData(login_User.UserID1, login_User.UserName1, login_User.AuthorLevel1, login_User.Password1);

            MessageBox.Show("Add data completed");
            Clear();
        }
        private bool CheckWhetherBox()
        {
            if (String.IsNullOrEmpty(txtUserID.Text))
            {
                lblContent += lblUserID.Content;
                return true;
            }
            else if (String.IsNullOrEmpty(txtUsername.Text))
            {
                lblContent += lblUserName.Content;
                return true;
            }
            else if (string.IsNullOrEmpty(cboLevelAuthor.Text))
            {
                lblContent += lblAuthor.Content;
                return true;
            }
            else if (String.IsNullOrEmpty(txtPassword.Password))
            {
                lblContent += lblPassword.Content;
                return true;
            }
            else if (string.IsNullOrEmpty(txtPassword2.Password))
            {
                lblContent += lblPassword2.Content;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void FillComboBox(string[] array, ComboBox box)
        {
            foreach (string x in array)
            {
                box.Items.Add(x);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            string[] author = { "1", "2", "3", "4", "5" };
            FillComboBox(author, cboLevelAuthor);
        }
        private void Clear()
        {
            txtUserID.Text = "";
            txtUsername.Text = "";
            txtPassword.Password = "";
            txtPassword2.Password = "";
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
