using AutoMapper;
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
        private readonly IMapper _mapper;
        public VillaAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();

            return Ok(_mapper.Map<List<VillaDto>>(villaList));



        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseTypeAttribute(StatusCodes.Status200OK)]
        [ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest)]
        [ProducesResponseTypeAttribute(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDto>(villa));
        }
        [HttpPost]
        [ProducesResponseTypeAttribute(StatusCodes.Status201Created)]
        [ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest)]
        [ProducesResponseTypeAttribute(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa already exist");
                return BadRequest();
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            /* if(villaDto.Id > 0)
             {
                 return StatusCode(StatusCodes.Status500InternalServerError);
             }*/
            Villa model = _mapper.Map<Villa>(createDTO);

            /*  Villa model = new()
              {
                  Name = createDTO.Name,
                  Amenity = createDTO.Amenity,
                  Description = createDTO.Description,
                  ImageUrl = createDTO.ImageUrl,
                  Occupancy = (int)createDTO.Occupancy,
                  Rate    = createDTO.Rate,
                  Sqft = (int)createDTO.Sqft,


              };*/

            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest();
            }
            /*  var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
              villa.Name = villaDto.Name;
              villa.Sqft = villaDto.Sqft;
              villa.Occupancy = villaDto.Occupancy;*/
            Villa model = _mapper.Map<Villa>(updateDTO);

            /*        Villa model = new()
                    {

                        Name = updateDTO.Name,
                        Amenity = updateDTO.Amenity,
                        Description = updateDTO.Description,
                        Id = updateDTO.Id,
                        ImageUrl = updateDTO.ImageUrl,
                        Occupancy = (int)updateDTO.Occupancy,
                        Rate = updateDTO.Rate,
                        Sqft = (int)updateDTO.Sqft,
                    };*/
            _db.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id=int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);
            /* VillaUpdateDTO villaDto = new()
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
            */
            if (villa == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(villaDTO, ModelState);

            Villa model = _mapper.Map<Villa>(villaDTO);

            /*  Villa model = new()
              {
                  Name = villaDto.Name,
                  Amenity = villaDto.Amenity,
                  Description = villaDto.Description,
                  Id = villaDto.Id,
                  ImageUrl = villaDto.ImageUrl,
                  Occupancy = (int)villaDto.Occupancy,
                  Rate = villaDto.Rate,
                  Sqft = (int)villaDto.Sqft,


              };*/
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
