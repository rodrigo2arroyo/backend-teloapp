using Microsoft.AspNetCore.Mvc;
using TeloApi.Models;

namespace TeloApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private static readonly List<Hotel> Hotels = new List<Hotel>
        {
            new Hotel { Name = "Hotel A"},
            new Hotel { Name = "Hotel B" }
        };

        // GET: api/Hotel
        [HttpGet]
        [ActionName("GetAllHotels")] // Esto da un nombre claro a Swagger
        public ActionResult<IEnumerable<Hotel>> GetAll()
        {
            return Ok(Hotels);
        }
    }
}