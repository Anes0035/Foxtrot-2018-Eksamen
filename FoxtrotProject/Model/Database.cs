using FoxtrotProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        string message;
        readonly string s = @"SELECT COUNT(*) FROM Customer WHERE CVR = @cvr";
        readonly string t = @"SELECT COUNT(*) FROM Contract WHERE ContractID = @contractid";
        readonly string r = @"SELECT COUNT(*) FROM Catalog WHERE ProductName1 = @name";

        /// Method for Adding a customer
        #region Customer
        public bool AddCustomer(Customer customer)
        {
            
            SqlCommand command = new SqlCommand(s, connection);
            command.Parameters.AddWithValue("@cvr", customer.CVR);

            OpenConnection();
            int records = (int)command.ExecuteScalar();

            CloseConnection();

            if (records == 0)
            {
                OpenConnection();
                command.Parameters.Clear();
                command = new SqlCommand(@"insert into Customer (CVR , Name, Address, PhoneNumber, ContactPerson, GrossIncome)
		values (@cvr, @name, @address, @phonenumber, @contactperson, @grossincome)", connection);

                command.Parameters.AddWithValue("@cvr", customer.CVR);
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@address", customer.Address);
                command.Parameters.AddWithValue("@phonenumber", customer.TelephoneNumber);
                command.Parameters.AddWithValue("@contactperson", customer.ContactPerson);
                command.Parameters.AddWithValue("@grossincome", customer.GrossIncome);
                command.ExecuteNonQuery();

                CloseConnection();
                return true;
            }
            else
            {
                message = "Fejl - Kunde Eksisterer Allerede";
                LogAdd(message);
                CloseConnection();
                return false;


            }


        }

        /// Method for editing a customers details ( Not Finished ) 

        public void EditCustomer(Customer customer)
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand("UPDATE Customer SET CVR = @cvr, Name = @name, Address = @address, PhoneNumber = @phonenumber, ContactPerson = @contactperson, GrossIncome = @grossincome "+ "WHERE CVR = @cvr", connection);
                command.Parameters.AddWithValue("@cvr", customer.CVR);
                command.Parameters.AddWithValue("@name", customer.Name);
                command.Parameters.AddWithValue("@address", customer.Address);
                command.Parameters.AddWithValue("@phonenumber", customer.TelephoneNumber);
                command.Parameters.AddWithValue("@contactperson", customer.ContactPerson);
                command.Parameters.AddWithValue("@grossincome", customer.GrossIncome);
                command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                message = "Fejl under redigering af kunde";
                LogAdd(message);
                // Create an Exception if customer isnt in the database
                
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
                SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE CVR = @cvr", connection);
                command.Parameters.Add(new SqlParameter("@cvr", customer.CVR));
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                message = "Fejl under sletning af kunde";
                LogAdd(message);
                // Create an exception if no Customer is selected
                throw;
            }
            finally
            {

                CloseConnection();
            }

        }
        public List<Customer> Customers()
        {

            OpenConnection();
            List<Customer> customers = new List<Customer>();
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
                    customer.GrossIncome = (int)sqlDataReader["GrossIncome"];
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

            #endregion
            #region Product

            public bool AddProduct(Product product)
            {

                SqlCommand command = new SqlCommand(r, connection);
                command.Parameters.AddWithValue("@name", product.Name);
                OpenConnection();
                int records = (int)command.ExecuteScalar();

                if (records == 0)
                {
                    command.Parameters.Clear();
                    command = new SqlCommand(@"Insert INTO Catalog(CompanyID, ProductID, ProductName1, ProductDesreptionLong, Price, ProductGroup, Discount)  
                                                                Values(@companyid, @productid, @name, @description, @price, @productgroup, @discount)", connection);
                    command.Parameters.AddWithValue("@companyid", 38168);
                    command.Parameters.AddWithValue("@productid", product.ID);
                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Parameters.AddWithValue("@description", product.Description);
                    command.Parameters.AddWithValue("@price", product.Price);
                    command.Parameters.AddWithValue("@productgroup", product.Category);
                    command.Parameters.AddWithValue("@discount", 1);
                    command.ExecuteNonQuery();

                    CloseConnection();
                    return true;
                }
                else
                {
                message = "Fejl under oprettelsen af produkt";
                LogAdd(message);
                    CloseConnection();
                    return false;


                }
            }

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
                message = "Fejl under redigering af produkt";
                LogAdd(message);
                    return false;
                }
                finally
                {
                    CloseConnection();
                }
            }

            public void RemoveProduct(Product product)
            {
                try
                {

                    OpenConnection();
                    SqlCommand command = new SqlCommand("DELETE FROM Catalog WHERE ProductID = @id", connection);
                    command.Parameters.Add(new SqlParameter("@id", product.ID));
                    command.ExecuteNonQuery();



                }
                catch (Exception)
                {
                message = "Fejl under sletning af produkt";
                    throw;
                }
                finally
                {
                    CloseConnection();
                }
            }

            public List<Product> Products()
            {
                List<Product> _products = new List<Product>();
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
                        product.Description = sqlDataReader["ProductDesreptionLong"] != DBNull.Value ? (string)sqlDataReader["ProductDesreptionLong"] : null;
                        product.Price = (double)sqlDataReader["Price"];
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
            #endregion


            #region Contract
            public bool AddContract(Contract contract)
            {

                SqlCommand command = new SqlCommand(t, connection);
                command.Parameters.AddWithValue("@contractid", contract.ID);
                OpenConnection();
                int records = (int)command.ExecuteScalar();

                if (records == 0)
                {
                    command.Parameters.Clear();
                    command = new SqlCommand(@"Insert INTO Contract([ContractID], [StartDate], [Period], [Price], [Status], [Subscription], [ProductGroups], [GetDiscount])  
                                                                Values(@contractid, @startDate, @period, @status, @subscription, @ProductGroups, @getDiscount))", connection);

                    command.Parameters.AddWithValue("@startDate", contract.StartDate);
                    command.Parameters.AddWithValue("@Period", contract.Period);
                    command.Parameters.AddWithValue("@status", contract.Status);
                    command.Parameters.AddWithValue("@subscription", contract.Subscription);
                    command.Parameters.AddWithValue("@contractid", contract.ID);
                    //command.Parameters.AddWithValue("@productGroups", contract.ProductGroups);
                    // command.Parameters.AddWithValue("@getDiscount", contract.GetDiscount);
                    command.ExecuteNonQuery();

                    CloseConnection();
                    return true;
                }
                else
                {
                message = "Fejl under oprettelse af Aftale";
                LogAdd(message);
                    CloseConnection();
                    return false;


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
                message = "Fejl under redigering af aftale";
                LogAdd(message);
                    return false;
                }
                finally
                {
                    CloseConnection();
                }
            }


            //CVR --- customer.CVR ?!?
            public void RemoveContract(Contract contract)
            {
                try
                {
                    OpenConnection();
                    SqlCommand command = new SqlCommand("DELETE FROM [dbo].[Contract] WHERE IdContract = @idContract", connection);
                    command.ExecuteNonQuery();
                    CloseConnection();

                }
                catch (Exception)
                {
                message = "Fejl under sletning af produkt";
                LogAdd(message);
                    throw;
                }
                finally
                {
                    CloseConnection();
                }
            }

            public ObservableCollection<Contract> Contracts()
            {
                ObservableCollection<Contract> contracts = new ObservableCollection<Contract>();
                OpenConnection();

                try
                {
                    SqlCommand command = new SqlCommand("Select * FROM Contract", connection);
                    SqlDataReader sqlDataReader = command.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Contract contract = new Contract();
                        Subscription subscription = new Subscription();

                        contract.StartDate = (DateTime)sqlDataReader["StartDate"];
                        contract.Status = (string)sqlDataReader["Status"];
                        contract.Discount = (int)sqlDataReader["Discount"];
                        subscription.Status = (bool)sqlDataReader["SubscriptionStatus"];
                        contract.ID = (int)sqlDataReader["ContractID"];
                        contracts.Add(contract);

                    }
                    return contracts;
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
      
        #endregion

        public void LogAdd(string message)
        {
            OpenConnection();
            try
            {
                LogWriter logwriter = new LogWriter();
                SqlCommand command = new SqlCommand(@"insert into Log (LogTime , LogMessage)
		values (@logtime, @logmessage)", connection);

                command.Parameters.AddWithValue("@logtime", logwriter.dt);
                command.Parameters.AddWithValue("@logmessage", message);
                command.ExecuteNonQuery();
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
        public ObservableCollection<LogReader> Logs()
        {
            ObservableCollection<LogReader> _logs = new ObservableCollection<LogReader>();
            try
            {
                LogWriter logwriter = new LogWriter();
                connection.Open();
                SqlCommand command = new SqlCommand("Select * FROM Log", connection);
                SqlDataReader sqlDataReader = command.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    LogReader logReader = new LogReader();

                  logReader.dt = (DateTime)sqlDataReader["LogTime"];
                     logReader.message = (string)sqlDataReader["LogMessage"];
              
                    _logs.Add(logReader);
                }

                return _logs;

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
    }
}


