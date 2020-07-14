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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void bookManageBtn_Click(object sender, RoutedEventArgs e)
        {
            BookManagement booksManagement = new BookManagement();
            booksManagement.Show();
        }

        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderManagement orderManagement = new OrderManagement();
            orderManagement.Show();
        }

        private void historyOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            TransactionPurchase transactionPurchase = new TransactionPurchase();
            transactionPurchase.Show();
        }

        private void lblAdminSetting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserRegister userRegister = new UserRegister();
            userRegister.Show();
        }

        private void lblLogout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void customerBtn_Click_1(object sender, RoutedEventArgs e)
        {
            CustomersManagement customers = new CustomersManagement();
            customers.Show();
        }
    }
}
