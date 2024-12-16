using Microsoft.AspNetCore.Mvc;
using TeloApi.Models;

namespace TeloApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransshipmentController : ControllerBase
    {
        private static readonly List<Transshipment> Transshipments = new List<Transshipment>
        {
            new Transshipment { Id = Guid.NewGuid(), ShipmentId = "A123", Status = "Completed" },
            new Transshipment { Id = Guid.NewGuid(), ShipmentId = "B456", Status = "In Progress" }
        };

        // GET: api/Transshipment
        [HttpGet]
        [ActionName("GetAllTransshipments")] // Esto asegura que Swagger use este nombre
        public ActionResult<IEnumerable<Transshipment>> GetAll()
        {
            return Ok(Transshipments);
        }

        // POST: api/Transshipment
        [HttpPost]
        [ActionName("CreateTransshipment")] // Swagger asignará un nombre claro
        public ActionResult<Transshipment> Create([FromBody] TransshipmentDto input)
        {
            var newTransshipment = new Transshipment
            {
                Id = Guid.NewGuid(),
                ShipmentId = input.ShipmentId,
                Status = input.Status
            };

            Transshipments.Add(newTransshipment);
            return CreatedAtAction(nameof(GetAll), new { id = newTransshipment.Id }, newTransshipment);
        }
        
        // GET: api/Transshipment/{id}
        [HttpGet("{id}")]
        [ActionName("SelectTrans")] // Swagger asignará un nombre claro
        public ActionResult<Transshipment> GetById(Guid id)
        {
            var transshipment = Transshipments.FirstOrDefault(t => t.Id == id);
            if (transshipment == null)
            {
                return NotFound();
            }
            return Ok(transshipment);
        }
    }
}