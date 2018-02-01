using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Pharmacy.Models.Pocos;
using Pharmacy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    /// <summary>  
    /// Reminders functions of McKenzies Pharmacy API
    /// </summary>  
    [Route("api/Reminders")]
    [Authorize]
    public class RemindersController : Controller
    {
        private readonly IRemindersService _remindersService;
        private readonly ICustomersService _customersService;

        /// <summary>  
        /// Constructor for Reminders functions of McKenzies Pharmacy API
        /// </summary>  
        public RemindersController(IRemindersService remindersService,
            ICustomersService customersService)
        {
            _remindersService = remindersService;
            _customersService = customersService;
        }

        /// <summary>  
        /// Add a new Customer
        /// </summary>  
        /// <param name="id">n</param>
        /// <returns code="200">Customer</returns>  // GET: api/Reminders/962ed775-a117-4e93-9d6c-7208bc5d484d
        [HttpGet]
        public async Task<IEnumerable<ReminderPoco>> GetCustomerReminders(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Customer customer = _customersService.GetCustomerByUsername(userId).Result;
            if (customer == null)
            {
                throw new Exception("Customer doesn't exist");
            }
            return await _remindersService.GetCustomerReminders(customer.CustomerId);
        }

        
    }
}
