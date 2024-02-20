using AlbarakaTestAPI.Data;
using AlbarakaTestAPI.Models;
using AlbarakaTestAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AlbarakaTestAPI.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : Controller
    {
        private UserRepository userRepository;

        public CustomerController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = userRepository.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("getCustomerById/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = userRepository.GetCustomerById(id);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            var (create,response_customer) = userRepository.CreateCustomer(customer);
            if (!create)
            {
                return BadRequest($"{response_customer.TCKNO} TCKNO Geçersiz");
            }

            return Ok(response_customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer updatedCustomer)
        {
            var existingCustomer = userRepository.UpdateCustomer(id, updatedCustomer);

            if (existingCustomer == null)
            {
                return BadRequest("Güncellemede Hata Oluştu!");
            }

            return Ok(existingCustomer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = userRepository.DeleteCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}
