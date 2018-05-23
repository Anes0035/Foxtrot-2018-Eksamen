using FoxtrotProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoxtrotProject.Model
{
   class Database
    {
        SqlConnection connection = new SqlConnection("Data Source =.; Initial Catalog = Foxtrot_SQLProject; Integrated Security = True");

        public void OpenConnection()
        {
            
            connection.Open();
        }

       public void CloseConnection()
        {
            connection.Close();
        }

        /// Method for Adding a customer

        public void AddCustomer(Customer customer)
        {
          

            try
            {
                OpenConnection();
                 SqlCommand command = new SqlCommand("Insert INTO Customer(CVR, Name, Address, PhoneNumber, ContactPerson, GrossIncome) Values(@cvr, @name, @address, @phonenumber, @contactperson, @grossincome)", connection);
                command.Parameters.AddWithValue("@cvr", customer.CVR);
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@address", customer.Address);
                command.Parameters.AddWithValue("@phonenumber", customer.TelephoneNumber);
                command.Parameters.AddWithValue("@contactperson", customer.ContactPerson);
                command.Parameters.AddWithValue("@grossincome", customer.GrossIncome);
                command.ExecuteNonQuery();             

            }
            catch (Exception)
            {
                //////MessageBox.Show("Fejl under oprettelse af kunde");
                
            }
            finally
            {
                CloseConnection();
            }
        }
              

        /// Method for editing a customers details ( Not Finished ) 

        public void EditCustomer(Customer customer)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("UPDATE Customer SET CVR = @cvr, Name = @name, Address = @address, PhoneNumber = @phonenumber, ContactPerson = @contactperson, GrossIncome = @grossincome ");
                command.Parameters.AddWithValue("@cvr", customer.CVR);
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@address", customer.Address);
                command.Parameters.AddWithValue("@phonenumber", customer.TelephoneNumber);
                command.Parameters.AddWithValue("@contactperson", customer.ContactPerson);
                command.Parameters.AddWithValue("@grossincome", customer.GrossIncome);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                // Create an Exception if customer isnt in the database
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        ///  Method for removing a customer

        public void RemoveCustomer(Customer customer)
        {
            try
            {

                OpenConnection();
                SqlCommand command = new SqlCommand("DELETE FROM Customer", connection);
       
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // Create an exception if no Customer is selected
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        public ObservableCollection<Customer> Customers()
        {

            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
           

        
                OpenConnection();

            try
            {
                SqlCommand command = new SqlCommand("Select * FROM Customer", connection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Customer customer = new Customer();
                    customer.CVR = (int)sqlDataReader["CVR"];
                    customer.Name = (string)sqlDataReader["Name"];
                    customer.Address = (string)sqlDataReader["Address"];
                    customer.TelephoneNumber = (int)sqlDataReader["PhoneNumber"];
                    customer.ContactPerson = (string)sqlDataReader["ContactPerson"];
                    //customer.GrossIncome = (int)sqlDataReader["GrossIncome"];
                    customers.Add(customer);

                }
                return customers;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
     
        }
            
           
        public string AddProduct(Product product)
        {
        try
        {
               
            SqlCommand command = new SqlCommand("Insert INTO Catalog([ProductID], [ProductName1], [ProductDescriptionLong], [Price], [ProductGroup]) " +
                                                                "Values(@id, @name, @description, @price, @category)", connection);
            command.Parameters.AddWithValue("@id", product.ID);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@category", product.Category);
            command.ExecuteNonQuery();
            CloseConnection();
                          
            return "Successfully Inserted";               
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                CloseConnection();
            }

        }           
        
        //public void GetAllProducts()
        //{

        //}
       
        public bool EditProduct(Product product)
        {
            OpenConnection();
            try
            {
                SqlCommand command = new SqlCommand("UPDATE Catalog SET [ProductID] = @id, [ProductName1] = @name, [ProductDescriptionLong] = @description, [Price] = @price, [ProductGroup] = @category");
                command.Parameters.AddWithValue("@id", product.ID);
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@category", product.Category);
                command.ExecuteNonQuery();
                CloseConnection();

                return true;
            }
          
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool RemoveProduct(int id)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("DELETE FROM Catalog WHERE Id = @id", connection);
                command.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        public ObservableCollection<Product> Products() 
        {

            ObservableCollection<Product> _products = new ObservableCollection<Product>();


            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * FROM Catalog", connection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Product product = new Product();

                    product.ID = (int)sqlDataReader["ProductID"];
                    product.Name = (string)sqlDataReader["ProductName1"];
                    product.Description = sqlDataReader["ProductDesreptionLong"] != DBNull.Value ? (string) sqlDataReader["ProductDesreptionLong"] : null ;
                    product.Price = (double) sqlDataReader["Price"];
                    product.Category = (string)sqlDataReader["ProductGroup"];
                    _products.Add(product);
                }

                return _products;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

  


        public string AddContract(Contract contract)
        {
            try
            {
                SqlCommand command = new SqlCommand("Insert CONTRACT [dbo].[Contract]([StartDate], [Period], [Status], [Subscription], [ProductGroups], [GetDiscount]) " +
                                                                    "Values(@startDate, @period, @status, @subscription, @ProductGroups, @getDiscount)", connection);
                command.Parameters.AddWithValue("@startDate", contract.StartDate);
                command.Parameters.AddWithValue("@Period", contract.Period);
                command.Parameters.AddWithValue("@status", contract.Status);
                command.Parameters.AddWithValue("@subscription", contract.Subscription);
                command.Parameters.AddWithValue("@productGroups", contract.ProductGroups);
               // command.Parameters.AddWithValue("@getDiscount", contract.GetDiscount);
                command.ExecuteNonQuery();
                CloseConnection();

                return "Successfully Inserted";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                CloseConnection();
            }

        }

        public bool UpdateContract(Contract contract)
        {
            OpenConnection();
            try
            {
                SqlCommand command = new SqlCommand("UPDATE [dbo].[Contract] SET [Status] = @status, [Subscription] = @subscription");
                command.Parameters.AddWithValue("@status", contract.Status);
                command.Parameters.AddWithValue("@subscription", contract.Subscription);
                                
                command.ExecuteNonQuery();
                CloseConnection();

                return true;
            }

            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }


        //CVR --- customer.CVR ?!?
        public bool RemoveContract(int idContract)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Contract] WHERE IdContract = @idContract", connection);
                command.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
