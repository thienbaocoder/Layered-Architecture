using LayeredArchitecture.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LayeredArchitecture.Models;

namespace LayeredArchitecture.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetAll()
        {
            var customers = _service.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(string id)
        {
            var c = _service.GetCustomerById(id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            try
            {

                _service.CreateCustomer(customer);
                return Ok(customer);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Customer customer)
        {
            if (customer.Id != id)
                return BadRequest("Mismatched ID");

            _service.UpdateCustomer(customer);
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _service.DeleteCustomer(id);
            return Ok();
        }
    }
}
