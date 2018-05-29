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
using FoxtrotProject.ViewModel;
using Microsoft.Win32;

namespace FoxtrotProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CustomerViewModel customerViewModel;
        ProductViewModel productViewModel;
        ContractViewModel contractViewModel;
        LogViewModel logViewModel;
        public MainWindow()
        {
            InitializeComponent();
            customerViewModel = new CustomerViewModel();
            productViewModel = new ProductViewModel();
            contractViewModel = new ContractViewModel();
            logViewModel = new LogViewModel();
            DataContext = new { customerViewModel, productViewModel, contractViewModel, logViewModel };
        }

        private void txtSearchCustomer_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = string.Empty;
            txt.Foreground = Brushes.Black;
            txt.GotFocus -= txtSearchCustomer_GotFocus;
        }
      
        private void TxtBox_Search_Product_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = string.Empty;
            txt.Foreground = Brushes.Black;
            txt.GotFocus -= TxtBox_Search_Product_GotFocus;
        }

    }
}
