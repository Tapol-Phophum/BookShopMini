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

            user.UserID = txtUserID.Text;
            user.UserName = txtUsername.Text;
            user.AuthorLevel = cboLevelAuthor.Text;
            user.Password = txtPassword.Text;
            // ทำการบันทึกข้อมูล
            User.AddData(user.UserID, user.UserName, user.AuthorLevel, user.Password);


            MessageBox.Show("Add data completed");
            Clear();
            searchBtn_Click (sender, e);

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
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUserID.Text))
            {
                MessageBox.Show("Please input 'UserID' for delete");
            }
            else if(DialogBox.Confirm("Delete",txtUserID.Text))
            {
                User.GetData(txtUserID.Text);
                User.DeleteData(txtUserID.Text);
                MessageBox.Show("Delete user : " + txtUserID.Text + " complete", "Delete complete");
                Clear();
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
               // List<User> user = new List<User>();
               List<DataList> user = new List<DataList>();
                //List<Table> items = new List<Table>();
                int numberOfList = dataFound.Count();
                for (int j = 0; j < numberOfList; j++)
                {
                    user.Add(new DataList(dataFound[j][0], dataFound[j][1], dataFound[j][2], dataFound[j][3]));
                }
                customersListView.ItemsSource = user;
            }
        }
    }
}
