using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<IActionResult> GetShops()
        {
            try
            {
                return Ok(await _service.GetShops());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Titles")]
        public async Task<IActionResult> GetTitles()
        {
            try
            {
                return Ok(await _service.GetTitles());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Practices
        [HttpGet]
        [Route("api/Practices")]
        public async Task<IActionResult> GetPractices()
        {
            try
            {
                return Ok(await _service.GetPractices());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Doctors
        [HttpGet]
        [Route("api/Doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            try
            {
                return Ok(await _service.GetDoctors());
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Practices/5/Doctors
        [HttpGet]
        [Route("api/Practices/{practiceId}/Doctors")]
        public async Task<IActionResult> GetDoctors(Guid practiceId)
        {
            try
            {
                return Ok(await _service.GetDoctorsByPractice(practiceId));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Addresses/SW6%201JL
        [HttpGet]
        [Route("api/Addresses/{postCode}")]
        public async Task<IActionResult> GetAddresses(string postCode)
        {
            try
            {
                return Ok(await _service.GetAddressesByPostcode(postCode));
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
