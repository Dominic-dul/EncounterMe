﻿using Api.Repositories;
using EncounterMeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<IEnumerable<MyLocation>> GetLocations()
        {
            try
            {
                return await _locationRepository.Get();
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        
        //GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyLocation>> GetSingle(int id)
        {
            return await _locationRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<MyLocation>> Post([FromBody] MyLocation value)
        {
            try
            {
                var newLocation = await _locationRepository.Create(value);
                return CreatedAtAction(nameof(GetLocations), new { id = newLocation.Id }, newLocation);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MyLocation>> Put(int id, [FromBody] MyLocation value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            await _locationRepository.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult<MyLocation>> Delete(int id)
        {
            try
            {
                var locationToDelete = await _locationRepository.Get(id);
                if (locationToDelete == null)
                {
                    return NotFound();
                }

                await _locationRepository.Delete(locationToDelete.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                
            }

            return null;
        }
    }
}
