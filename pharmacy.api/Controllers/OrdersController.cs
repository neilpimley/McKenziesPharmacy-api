using System;
using Pharmacy.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Models.Pocos;
using Pharmacy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    /// <summary>  
    /// Orders functions of McKenzies Pharmacy API
    /// </summary>  
    //[Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _service;
        private readonly ICustomersService _customersService;

        /// <summary>  
        /// Constructor for Orders functions of McKenzies Pharmacy API
        /// </summary>  
        public OrdersController(IOrdersService service, ICustomersService customersService)
        {
            _service = service;
            _customersService = customersService;
        }


        // GET: api/Orders
        [HttpGet]
        [Route("api/Order")]
        public async Task<IActionResult> GetOrder()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var customer = await _customersService.GetCustomerByUsername(userId);
            if (customer == null)
            {
                return NotFound();
            }
            Order order = await _service.GetCurrentOrder(customer.CustomerId, (int)Status.Inbasket);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // GET: api/Orders
        [HttpGet]
        [Route("api/Order/{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            try
            {
                return Ok( await _service.GetOrder(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Orders")]
        public async Task<IActionResult> GetOrders()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var customer = _customersService.GetCustomerByUsername(userId).Result;
            if (customer == null)
            {
                return BadRequest("Customer doesn't exist");
            }

            try
            {
                return Ok(await _service.GetOrders(customer.CustomerId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Orders/962ed775-a117-4e93-9d6c-7208bc5d484d
        [HttpGet]
        [Route("api/OrderLines/{id}", Name = "GetOrderLines")]
        public async Task<IActionResult> GetOrderLines(Guid id)
        {
            try
            {
                return Ok(await _service.GetOrderLines(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Orders/5
        [HttpPut]
        [Route("api/Order/{id}", Name = "SubmitOrder")]
        public async Task<IActionResult> SubmitOrder(Guid id, bool emailReminder = false, bool smsReminder = false)
        {
            var order = await _service.GetOrder(id);
            if (order == null)
            {
                return BadRequest("Order does not exist");
            }
            order.SmsReminder = smsReminder;
            order.EmailReminder = emailReminder;
            try
            {
                await _service.SubmitOrder(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(order);
        }

        // POST: api/Orders
        [HttpPost]
        [Route("api/OrderLines")]
        public async Task<IActionResult> AddOrderLineOrder([FromBody]OrderLine orderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                await _service.AddToOrder(orderLine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(orderLine);
        }

        // DELETE: api/Orders/2F714D7E-1CD6-4E73-98A7-98F875D558F6
        [HttpDelete]
        [Route("api/OrderLines/{id}")]
        public async Task<IActionResult> DeleteOrderLine(Guid id)
        {
            OrderLine orderLine = await _service.GetOrderLine(id);
            if (orderLine == null)
            {
                return NotFound();
            }

            try
            {
                await _service.DeleteFromOrder(orderLine);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(orderLine);
        }

        
    }
}