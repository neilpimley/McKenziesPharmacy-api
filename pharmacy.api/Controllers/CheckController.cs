using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    public class CheckController : Controller
    {
        [HttpGet]
        [Route("api/CheckServer")]
        public IActionResult CheckServer()
        {
            return Ok();
        }
    }

}