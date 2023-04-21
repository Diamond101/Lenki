using System.Transactions;
using LenkiData.Interface;
using LenkiMicroservice.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LenkiMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomers _customersRepository;
       
        public CustomersController(ICustomers customersRepository)
        {
            _customersRepository = customersRepository;
        }
        
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            var customers = _customersRepository.GetCustomers();
            return new OkObjectResult(customers);
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public IActionResult Get(int id)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            var customer = _customersRepository.GetCustomerByID(id);
            return new OkObjectResult(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User customer)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            using (var scope = new TransactionScope())
            {                
                _customersRepository.InsertCustomer(customer);
                scope.Complete();
                return CreatedAtAction(nameof(Get), 
                    new { FullName = customer.FullName }, customer);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Users customer)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            if (customer != null)
            {
                using (var scope = new TransactionScope())
                {
                    _customersRepository.UpdateCustomer(customer);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var username = HttpContext.User;
            if (username == null)
            {
                return Unauthorized();
            }
            _customersRepository.DeleteCustomer(id);
            return new OkResult();
        }

    }
}