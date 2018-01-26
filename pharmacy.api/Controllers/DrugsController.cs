using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Models.Pocos;
using Pharmacy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    /// <summary>  
    /// Drugs related functions of McKenzies Pharmacy API
    /// </summary>  
    [Route("api/Drugs")]
    [Authorize]
    public class DrugsController : Controller
    {
        private readonly IDrugsService _service;
        private readonly ICustomersService _customerService;
        
        /// <summary>  
        /// Constructor for Drugs functions of McKenzies Pharmacy API
        /// </summary>  
        public DrugsController(IDrugsService service, ICustomersService customerService)
        {
            _service = service;
            _customerService = customerService;
        }

        /// <summary>  
        /// Get a list of drugs
        /// </summary>  
        /// <param name="drugName"></param>
        /// <returns code="200"></returns>  
        // GET: api/Drugs
        [HttpGet]
        public IEnumerable<DrugPoco> GetDrugs(string drugName)
        {
            var userID = User.Identity.Name;
            var customer = _customerService.GetCustomerByUsername(userID);
            return _service.GetDrugs(customer.CustomerId, drugName);
        }
       
    }
}