﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TravelWeb.Models;

namespace TravelWeb.Controllers.api
{
    public class PlacesController : ApiController
    {
        private readonly DatabaseEntities _db = new DatabaseEntities();

        // GET: api/Places
        public IQueryable<Place> GetPlaces()
        {
            return _db.Places;
        }

        // GET: api/Places/5
        [ResponseType(typeof(Place))]
        public async Task<IHttpActionResult> GetPlace(int id)
        {
            Place place = await _db.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }

        // PUT: api/Places/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlace(int id, Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != place.PlaceId)
            {
                return BadRequest();
            }

            _db.Entry(place).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Places
        [ResponseType(typeof(Place))]
        public async Task<IHttpActionResult> PostPlace(Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Places.Add(place);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = place.PlaceId }, place);
        }

        // DELETE: api/Places/5
        [ResponseType(typeof(Place))]
        public async Task<IHttpActionResult> DeletePlace(int id)
        {
            Place place = await _db.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }

            _db.Places.Remove(place);
            await _db.SaveChangesAsync();

            return Ok(place);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlaceExists(int id)
        {
            return _db.Places.Count(e => e.PlaceId == id) > 0;
        }
    }
}