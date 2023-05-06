using MagicVilla_VillaAAPI.Data;
using MagicVilla_VillaAAPI.Logging;
using MagicVilla_VillaAAPI.Model;
using MagicVilla_VillaAAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public VillaAPIController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(_db.Villas);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseTypeAttribute(StatusCodes.Status200OK)]
        [ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest)]
        [ProducesResponseTypeAttribute(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDto> GetVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
           var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseTypeAttribute(StatusCodes.Status201Created)]
        [ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest)]
        [ProducesResponseTypeAttribute(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null){
                ModelState.AddModelError("", "Villa already exist");
                return BadRequest();
            }
            if(villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if(villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Villa model = new()
            {
                Name = villaDto.Name,
                Amenity = villaDto.Amenity,
                Description = villaDto.Description,
                Id = villaDto.Id,
                ImageUrl = villaDto.ImageUrl,
                Occupancy = (int)villaDto.Occupancy,
                Rate    = villaDto.Rate,
                Sqft = (int)villaDto.Sqft,


            };
            _db.Villas.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla",new { id = villaDto.Id},villaDto);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDto villaDto)
        {
            if(villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }
            /*  var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
              villa.Name = villaDto.Name;
              villa.Sqft = villaDto.Sqft;
              villa.Occupancy = villaDto.Occupancy;*/
            Villa model = new()
            {

                Name = villaDto.Name,
                Amenity = villaDto.Amenity,
                Description = villaDto.Description,
                Id = villaDto.Id,
                ImageUrl = villaDto.ImageUrl,
                Occupancy = (int)villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = (int)villaDto.Sqft,
            };
            _db.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id=int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if(patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);

            VillaDto villaDto = new()
            {
                Name = villa.Name,
                Amenity = villa.Amenity,
                Description = villa.Description,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Occupancy = (int)villa.Occupancy,
                Rate = villa.Rate,
                Sqft = (int)villa.Sqft,


            };
            if (villa == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(villaDto, ModelState);

            Villa model = new()
            {
                Name = villaDto.Name,
                Amenity = villaDto.Amenity,
                Description = villaDto.Description,
                Id = villaDto.Id,
                ImageUrl = villaDto.ImageUrl,
                Occupancy = (int)villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = (int)villaDto.Sqft,


            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
