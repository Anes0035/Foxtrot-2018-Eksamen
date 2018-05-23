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
        ProductViewModel productViewModel;
        CustomerViewModel customerViewModel;
        
        public MainWindow()
        {
            InitializeComponent();
            

            productViewModel = new ProductViewModel();
            customerViewModel = new CustomerViewModel();
          
            DataContext = customerViewModel;
        }

        private void txtSearchCustomer_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = string.Empty;
            txt.Foreground = Brushes.Black;
            txt.GotFocus -= txtSearchCustomer_GotFocus;
        }


        //????
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Produktet er oprettet");
        //}

                   

        private void Button_Vis_Log_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "DataLog(.txt)|*.txt";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                TextBox_Log_File.Text = filename;

                Paragraph parag = new Paragraph();
                parag.Inlines.Add(System.IO.File.ReadAllText(filename));
            
            }

        }

        //private void Btn_Save_Product_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        productViewModel.AddProduct();
        //        txtName.Text = string.Empty;
        //        txtDescription.Text = string.Empty;
        //        txtCategory.Text = string.Empty;
        //        txtPrice.Text = string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void Btn_UpDate_Product_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        productViewModel.UpDateProduct();
        //        txtName.Text = string.Empty;
        //        txtDescription.Text = string.Empty;
        //        txtCategory.Text = string.Empty;
        //        txtPrice.Text = string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        
        //}
        //private void Btn_Search_Product_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
               
        //        //productViewModel.Search_Product();
        //        TxtBox_Search_Product.Text = string.Empty;
        //    }
        //    catch (Exception )
        //    {
        //        MessageBox.Show("Error Search", "Error", MessageBoxButton.OK);
        //    }
        //}
      

        //private void Btn_Delete_Product_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        productViewModel.DeleteProduct();
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Error Delete", "Error", MessageBoxButton.OK);
        //    }
        //}

        private void TxtBox_Search_Product_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = string.Empty;
            txt.Foreground = Brushes.Black;
            txt.GotFocus -= TxtBox_Search_Product_GotFocus;
        }
    }
}
