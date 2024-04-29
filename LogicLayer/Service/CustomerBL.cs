using CommonLayer.Model;
using LogicLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Service
{
    public class CustomerBL : ICustomerBL
    {
        private readonly ICustomerRL icustomerRL;
        public CustomerBL(ICustomerRL icustomerRL)
        {
            this.icustomerRL = icustomerRL;
        }

        //Create
        public CustomerEntity CustomerRegistration(CustomerRegistrationModel Insert)
        {
            try
            {
                return icustomerRL.CustomerRegistration(Insert);
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
                return icustomerRL.RetrieveData();
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
                return icustomerRL.UpdateData(CustomerId, update);
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
                return icustomerRL.DeleteData(CustomerId);
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
                return icustomerRL.GetById(CustomerId);
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
                return icustomerRL.CountOfCustomers();
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
                return icustomerRL.EmailExists(Email);
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
                return icustomerRL.CustomerLogin(login);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
