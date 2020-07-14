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
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Window
    {
        public OrderManagement()
        {
            InitializeComponent();
        }

        private void moneyChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            Cashier cashier = new Cashier();
            cashier.Show();
        }
    }
}
