using LenkiMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LenkiData.Interface
{
  public  interface ICustomers
    {
        IEnumerable<Users> GetCustomers();
        Users GetCustomerByID(int customers);
        void InsertCustomer(User customer);
        void DeleteCustomer(int CustomerId);
        void UpdateCustomer(Users customer);
        void Save();
    }
}
