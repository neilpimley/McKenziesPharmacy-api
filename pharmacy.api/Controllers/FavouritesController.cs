using System;
using Pharmacy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Models.Pocos;
using Pharmacy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    /// <summary>  
    /// Users favourites functions of McKenzies Pharmacy API
    /// </summary>  
    [Route("api/Favourites")]
    [Authorize]
    public class FavouritesController : Controller
    {
        private readonly IFavouritesService _service;
        private readonly ICustomersService _customerService;

        /// <summary>  
        /// Constructor for Favourites functions of McKenzies Pharmacy API
        /// </summary>  
        public FavouritesController(IFavouritesService service, ICustomersService customerService)
        {
            _service = service;
            _customerService = customerService;
        }

        /// <summary>  
        /// Get a list of drugs favouited by a user
        /// </summary>  
        /// <returns code="200"></returns>  
        // GET: api/Favourites
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userID = User.Identity.Name;
            var customer = await _customerService.GetCustomerByUsername(userID);
            if (customer == null)
            {
                throw new Exception("Customer doesn't exist");
            }
            return Ok(_service.GetFavouriteDrugs(customer.CustomerId));
        }

        /// <summary>  
        /// Add a user favourite drug
        /// </summary>  
        /// <param name="favourite"></param>
        /// <returns code="200"></returns>  
        // POST: api/Favourites
        [HttpPost]
        public async Task<IActionResult> Post(Favourite favourite)
        {
            var userID = User.Identity.Name;
            var customer = await _customerService.GetCustomerByUsername(userID);
            if (customer == null)
            {
                return NotFound();
            }
            favourite.FavouriteId = Guid.NewGuid();
            favourite.CustomerId = customer.CustomerId;
            favourite.CreatedOn = DateTime.Now;

            try
            {
                await _service.AddFavourite(favourite);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); ;
            }

            return CreatedAtRoute("DefaultApi", new { id = favourite.FavouriteId }, favourite);
        }

        /// <summary>  
        /// Remove a favourite drug from a user profile
        /// </summary>  
        /// <param name="id"></param>
        /// <returns code="200"></returns>  
        // DELETE: api/Favourites/5
        [HttpDelete]
        public async Task<IActionResult> DeleteFavourite(Guid id)
        {
            Favourite favourite = await _service.GetFavourite(id);
            if (favourite == null)
            {
                return NotFound();
            }

            try {
                await _service.DeleteFavourite(favourite.FavouriteId);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            return Ok(favourite);
        }

    }
}