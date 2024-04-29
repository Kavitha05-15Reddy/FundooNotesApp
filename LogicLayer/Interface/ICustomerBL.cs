using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface
{
    public interface ICustomerBL
    {
        public CustomerEntity CustomerRegistration(CustomerRegistrationModel Insert);
        public object RetrieveData();
        public bool UpdateData(long CustomerId, CustomerUpdateModel update);
        public bool DeleteData(long CustomerId);
        public object GetById(long CustomerId);
        public int CountOfCustomers();
        public object EmailExists(string Email);
        public string CustomerLogin(CustomerLoginModel login);
    }
}
