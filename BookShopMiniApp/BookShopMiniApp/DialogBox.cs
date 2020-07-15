using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookShopMiniApp
{
    class DialogBox
    {
        public static bool Confirm(string command, string index)
        {
            var result = MessageBox.Show("Do you want to "+ command + " "+ index + "?", command + " Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
