using FoxtrotProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxtrotProject.Model
{
    class Database
    {
        private SqlConnection connection;

        private void OpenConnection()
        {
            SqlConnection connection = new SqlConnection("Data Source =.; Initial Catalog = Foxtrot_SQLProject; Integrated Security = True");
            connection.Open();
        }

        private void CloseConnection()
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
                // Create an Exception if Customer already exists
                throw;
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

        public void RemoveCustomer()
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
    }
}
