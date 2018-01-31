using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Models.Pocos;
using Pharmacy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    /// <summary>  
    /// Customer functions of McKenzies Pharmacy API
    /// </summary>  
    // [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomersService _service;


        /// <summary>  
        /// Constructor for Customer functions of McKenzies Pharmacy API
        /// </summary>  
        public CustomersController(ICustomersService service)
        {
            _service = service;
        }

        /// <summary>  
        /// Get a Customer
        /// </summary>  
        /// <param name="userid"></param>
        /// <returns code="200">Customer</returns>  
        // GET: api/Customers/5
        [HttpGet]
        [Route("api/Customers")]
        public async Task<IActionResult> GetCustomer(string userid)
        {
            CustomerPoco customer = await _service.GetCustomerByUsername(userid);
            if (customer == null)
            {
                return BadRequest("Customer has not registered yet");
            }
            return Ok(customer);
        }

        /// <summary>  
        /// Update a Customer
        /// </summary>  
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns code="200">Customer</returns>  
        // PUT: api/Customers/5
        [HttpPut]
        [Route("api/Customers/{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, CustomerPoco customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            
            try
            {
               await _service.UpdateCustomerDetails(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(customer);
        }

        /// <summary>  
        /// Add a new Customer
        /// </summary>  
        /// <param name="customer"></param>
        /// <returns code="200">Customer</returns>  
        // POST: api/Customers
        [HttpPost]
        [Route("api/Customers")]
        public async Task<IActionResult> PostCustomer([FromBody]CustomerPoco customer)
        {
            try
            {
                customer = await _service.RegisterCustomer(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(customer);
        }
    }
}