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

namespace FoxtrotProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductViewModel productViewModel;
        CustomerViewModel customerViewModel;
        public MainWindow()
        {
            InitializeComponent();
            

            productViewModel = new ProductViewModel();
            customerViewModel = new CustomerViewModel();

            DataContext = customerViewModel;
        }

        private void txtSearchCostumer_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = string.Empty;
            txt.Foreground = Brushes.Black;
            txt.GotFocus -= txtSearchCostumer_GotFocus;
        }
    }
}
