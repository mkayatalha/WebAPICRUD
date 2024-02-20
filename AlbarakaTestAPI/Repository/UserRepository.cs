using AlbarakaTestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlbarakaTestAPI.Helper;
using AlbarakaTestAPI.Data;

namespace AlbarakaTestAPI.Repository    
{
    public class UserRepository
    {

        private ApplicationDbContext context;
        private DbSet<Customer> customers;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            customers = context.Set<Customer>();
        }
        public List<Customer> GetCustomers()
        {
            return customers.ToList();
        }
        public Customer GetCustomerById(int id)
        {
            Customer customer = customers.FirstOrDefault( x => x.CustomerID == id);

            return customer;
        }

        public Customer DeleteCustomer(int id)
        {
            var customer = GetCustomerById(id);

            if (customer == null)
            {
                return customer;
            }

            context.Remove(customer);
            context.SaveChanges();

            return customer;
        }

        public Customer UpdateCustomer(int id, Customer updatedCustomer)
        {
            var existingCustomer = GetCustomerById(id);
            
            if (existingCustomer == null || !CheckTCNumber.ValidateTCNumber(updatedCustomer.TCKNO) || updatedCustomer.CustomerID != existingCustomer.CustomerID)
            {
                return existingCustomer;
            }

            existingCustomer.Name = existingCustomer.Name;
            existingCustomer.Age = updatedCustomer.Age;
            existingCustomer.Surname = updatedCustomer.Surname;
            existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;
            existingCustomer.TCKNO = updatedCustomer.TCKNO;

            context.SaveChanges();
            return updatedCustomer;
        }

        public (bool,Customer) CreateCustomer(Customer customer)
        {

            if (!CheckTCNumber.ValidateTCNumber(customer.TCKNO))
            {
                return (false,customer);
            }

            customers.Add(customer);
            context.SaveChanges();
            return (true,customer);
        }
    }
}
