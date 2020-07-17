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
using System.Windows.Shapes;

namespace BookShopMiniApp
{
    /// <summary>
    /// Interaction logic for UserRegister.xaml
    /// </summary>
    public partial class UserRegister : Window
    {
        private List<List<string>> searchResult;
        string lblContent = "";
        private string userID;
        public UserRegister()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            if (CheckWhetherBox())
            {
                MessageBox.Show("Please Input data in :" + lblContent);
                lblContent = "";
                return;
            }
            else
            {
                searchResult = User.SearchItem("UserID", txtUserID.Text);

                List<List<string>> dataFound = new List<List<string>>();
                int i = 0;
                foreach (List<string> searchItem in searchResult)
                {
                    dataFound.Add(searchItem);
                    i++;
                }
                if (dataFound.Count == 0)
                {
                    user.UserID = txtUserID.Text;
                    user.UserName = txtUsername.Text;
                    user.AuthorLevel = cboLevelAuthor.Text;
                    user.Password = txtPassword.Text;

                    if (txtPassword.Text == txtPassword2.Text)
                    {
                        // ทำการบันทึกข้อมูล
                        User.AddData(user.UserID, user.UserName, user.AuthorLevel, user.Password);
                        MessageBox.Show("Successfully Add User");
                        Clear();
                        searchBtn_Click(sender, e);
                    }
                    else
                    {
                        lblMsg.Foreground = new SolidColorBrush(Colors.Red);
                        lblMsg.Content = "Not Mached";
                    }
                }
                else
                {
                    MessageBox.Show("Add user : " + txtUserID.Text + Environment.NewLine
                       + "Cannot Add New User: Because UserID is Unique", "Add  Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
            else if (String.IsNullOrEmpty(txtPassword.Text))
            {
                lblContent += lblPassword.Content;
                return true;
            }
            else if (string.IsNullOrEmpty(txtPassword2.Text))
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
            txtSearch.Text = "";
            txtUserID.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtPassword2.Text = "";
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            searchResult = User.SearchItem(txtSearch.Text);
            dataSearchShow(searchResult);
            Clear();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUserID.Text))
            {
                MessageBox.Show("Please input 'UserID' for delete");
            }
            else if(DialogBox.Confirm("Delete",txtUserID.Text))
            {
                searchResult = User.SearchItem("UserID", txtUserID.Text);
               
                List<List<string>> dataFound = new List<List<string>>();
                int i = 0;
                foreach (List<string> searchItem in searchResult)
                {
                    dataFound.Add(searchItem);
                    i++;
                }
                if (dataFound.Count == 0)
                {
                    MessageBox.Show("Data Not Found.");
                    Clear();
                }
                else
                {
                    User.DeleteData(txtUserID.Text);
                    MessageBox.Show("Delete user : " + txtUserID.Text + " complete", "Delete complete");
                    Clear();
                }
                searchBtn_Click(sender, e);
            }
        }
        public void dataSearchShow(List<List<string>> searchResult)
        {
            List<List<string>> dataFound = new List<List<string>>();
            int i = 0;
            foreach (List<string> searchItem in searchResult)
            {
                dataFound.Add(searchItem);
                i++;
            }
            if (dataFound.Count == 0)
            {
                MessageBox.Show("Data Not Found.");
            }
            else
            {
               List<User> user = new List<User>();
                //List<Table> items = new List<Table>();
                int numberOfList = dataFound.Count();
                for (int j = 0; j < numberOfList; j++)
                {
                    user.Add(new User(dataFound[j][0], dataFound[j][1], dataFound[j][2], dataFound[j][3]));
                }
                customersListView.ItemsSource = user;
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUserID.Text))
            {
                MessageBox.Show("Please input 'UserID' for Update");
            }
            else if (DialogBox.Confirm("Update", txtUserID.Text))
            {
                if (txtPassword.Text == txtPassword2.Text)
                {
                    User.UpdateData(txtUserID.Text, txtUsername.Text, cboLevelAuthor.Text, txtPassword.Text);
                    MessageBox.Show("Update user : " + txtUserID.Text + " complete", "Update complete");
                    Clear();
                    searchBtn_Click(sender, e);
                }
                else
                {
                    lblMsg.Foreground = new SolidColorBrush(Colors.Red);
                    MessageBox.Show("Update user : " + txtUserID.Text + Environment.NewLine
                    + "Cannot update Password Not Mached", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtPassword2_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPassword.Text == txtPassword2.Text)
            {
                lblMsg.Foreground = new SolidColorBrush(Colors.BlueViolet);
                lblMsg.Content = "Mached";
            }
            else
            {
                lblMsg.Foreground = new SolidColorBrush(Colors.Red);
                lblMsg.Content = "Not Mached";
            }
        }

        private void cboLevelAuthor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int IndexVal = cboLevelAuthor.SelectedIndex;
            switch (IndexVal)
            {
                case 0:
                    lblAuthorDetail.Content = "User";
                    break;
                case 1:
                    lblAuthorDetail.Content = "Staff";
                    break;
                case 2:
                    lblAuthorDetail.Content = "Leader";
                    break;
                case 3:
                    lblAuthorDetail.Content = "Suppervisor up";
                    break;
                case 4:
                    lblAuthorDetail.Content = "Management Level";
                    break;
                default:
                    break;
            }
        }

        private void customersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selectedUser = (User)customersListView.SelectedItem;
            if(selectedUser!=null)
            {
                this.userID = selectedUser.UserID;
                txtUserID.Text = selectedUser.UserID;
                txtUsername.Text = selectedUser.UserName;
                cboLevelAuthor.Text = selectedUser.AuthorLevel;
                txtPassword.Text = selectedUser.Password;
                txtSearch.Text = "";

            }
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
        }
    }
}
