using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CustomerRL : ICustomerRL
    {
        private readonly FundooContext fundooContext;
        public CustomerRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        //Create
        public CustomerEntity CustomerRegistration(CustomerRegistrationModel Insert)
        {
            try
            {
                CustomerEntity Customer = new CustomerEntity();

                Customer.CustomerName = Insert.CustomerName;
                Customer.Phone = Insert.Phone;
                Customer.City = Insert.City;
                Customer.Email = Insert.Email;
                Customer.Password = Insert.Password;

                fundooContext.CustomerTable.Add(Customer);
                int result = fundooContext.SaveChanges();
                if (result != 0)
                {
                    return Customer;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Retrieve
        public object RetrieveData()
        {
            try
            {
                var data = fundooContext.CustomerTable.ToList();
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Update
        public bool UpdateData(long CustomerId, CustomerUpdateModel update)
        {
            try
            {
                var result = fundooContext.CustomerTable.FirstOrDefault(u => u.CustomerId == CustomerId);
                if (result != null)
                {
                    result.CustomerName = update.CustomerName;
                    result.Phone = update.Phone;
                    result.City = update.City;
                    result.Email = update.Email;
                    result.Password = update.Password;

                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Delete
        public bool DeleteData(long CustomerId)
        {
            try
            {
                var result = fundooContext.CustomerTable.FirstOrDefault(d => d.CustomerId == CustomerId);
                if (result != null)
                {
                    fundooContext.CustomerTable.Remove(result);

                    fundooContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //GetById
        public object GetById(long CustomerId)
        {
            try
            {
                var data = fundooContext.CustomerTable.FirstOrDefault(x => x.CustomerId == CustomerId);
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Count
        public int CountOfCustomers()
        {
            try
            {
                int count = fundooContext.CustomerTable.Count();
                if (count > 0)
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //EmailExists
        public object EmailExists(string Email)
        {
            try
            {
                var result = fundooContext.CustomerTable.FirstOrDefault(x => x.Email == Email);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Login
        public string CustomerLogin(CustomerLoginModel login)
        {
            try
            {
                var result = fundooContext.CustomerTable.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);
                if (result != null)
                {
                    return "Login successfully";
                }
                else
                {
                    return "Login Failed";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
