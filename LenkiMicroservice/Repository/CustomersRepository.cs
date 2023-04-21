using LenkiData.Interface;
using LenkiMicroservice.DBContexts;
using LenkiMicroservice.Interface;
using LenkiMicroservice.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LenkiData.Repository
{
    public class CustomersRepository : ICustomers
    {
        private readonly LenkiDBContext _dbContext;

        public CustomersRepository(LenkiDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteCustomer(int customerId)
        {
            var category = _dbContext.Customers.Find(customerId);
            _dbContext.Customers.Remove(category);
            Save();
        }

        public IEnumerable<Users> GetCustomers()
        {
            return _dbContext.Customers.ToList();
        }

        public Users GetCustomerByID(int customer)
        {
            return _dbContext.Customers.Find(customer);
        }

        public void InsertCustomer(User customer)
        {
            Users users = new Users();
            users.FullName = customer.FullName;
            users.Password = customer.Password;
            users.UserRole = customer.UserRole;
            users.Email = customer.Email;
            users.Phone = customer.Phone;
            _dbContext.Add(users);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateCustomer(Users customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            Save();
        }
    }
}
