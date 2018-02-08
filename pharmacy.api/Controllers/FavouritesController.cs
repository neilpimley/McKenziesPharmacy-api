using System;
using Pharmacy.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    /// <summary>  
    /// Users favourites functions of McKenzies Pharmacy API
    /// </summary>  
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
        [Route("api/Favourites")]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var customer = await _customerService.GetCustomerByUsername(userId);
            if (customer == null)
            {
                return NotFound("Customer is not logged in");
            }
            return Ok(await _service.GetFavouriteDrugs(customer.CustomerId));
        }

        /// <summary>  
        /// Add a user favourite drug
        /// </summary>  
        /// <param name="favourite"></param>
        /// <returns code="200"></returns>  
        // POST: api/Favourites
        [HttpPost]
        [Route("api/Favourites")]
        public async Task<IActionResult> Post([FromBody]Favourite favourite)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var customer = await _customerService.GetCustomerByUsername(userId);
            if (customer == null)
            {
                return NotFound("Customer is not logged in");
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

            return Ok(favourite);
        }

        /// <summary>  
        /// Remove a favourite drug from a user profile
        /// </summary>  
        /// <param name="id"></param>
        /// <returns code="200"></returns>  
        // DELETE: api/Favourites/5
        [HttpDelete]
        [Route("api/Favourites/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Favourite favourite = await _service.GetFavourite(id);
            if (favourite == null)
            {
                return NotFound("Favourite has already been removed");
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