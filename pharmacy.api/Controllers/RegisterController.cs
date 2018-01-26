using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Services.Interfaces;
using Pharmacy.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    /// <summary>  
    /// Customer functions of McKenzies Pharmacy API
    /// </summary>  
    [Authorize]
    public class RegisterController : Controller
    {
        private readonly IRegisterService _service;

        /// <summary>  
        /// Constructor for Register functions of McKenzies Pharmacy API
        /// </summary>  
        public RegisterController(IRegisterService service)
        {
            _service = service;
        }


        // GET: api/Shops
        [HttpGet]
        [Route("api/Shops")]
        public IEnumerable<Shop> GetShops()
        {
            return _service.GetShops();
        }

        [HttpGet]
        [Route("api/Titles")]
        public IEnumerable<Title> GetTitles()
        {
            return _service.GetTitles();
        }

        // GET: api/Practices
        [HttpGet]
        [Route("api/Practices")]
        public IEnumerable<Practice> GetPractices()
        {
            return _service.GetPractices();
        }

        // GET: api/Doctors
        [HttpGet]
        [Route("api/Doctors")]
        public IEnumerable<Doctor> GetDoctors()
        {
            return _service.GetDoctors();
        }

        // GET: api/Practices/5/Doctors
        [HttpGet]
        [Route("api/Practices/{practiceId}/Doctors")]
        public IEnumerable<Doctor> GetDoctors(Guid practiceId)
        {
            return _service.GetDoctorsByPractice(practiceId);
        }

        // GET: api/Addresses/SW6%201JL
        [HttpGet]
        [Route("api/Addresses/{postCode}")]
        public async Task<IEnumerable<Address>> GetAddresses(string postCode)
        {
            return await _service.GetAddressesByPostcode(postCode);
        }

    }
}
