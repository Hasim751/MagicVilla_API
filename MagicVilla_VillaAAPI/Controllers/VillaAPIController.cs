﻿using MagicVilla_VillaAAPI.Data;
using MagicVilla_VillaAAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {

            return Ok(VillaStore.villaList);
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
           var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
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
            if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null){
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

            villaDto.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDto);
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
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
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
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDto.Name;
            villa.Sqft = villaDto.Sqft;
            villa.Occupancy = villaDto.Occupancy;
            return NoContent();
        }

        [HttpPatch("{id = int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if(patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(villa, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}