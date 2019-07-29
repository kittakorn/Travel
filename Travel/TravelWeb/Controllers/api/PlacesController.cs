using System;
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
        public IQueryable<object> GetPlaces()
        {
            var places = _db.Places.ToList().Select(x => new PlaceViewModel(x));
            return places.AsQueryable();
        }

        // GET: api/Places/5
        [ResponseType(typeof(Place))]
        public async Task<IHttpActionResult> GetPlace(int id)
        {
            Place findPlace = await _db.Places.FindAsync(id);
            if (findPlace == null)
            {
                return NotFound();
            }
            PlaceViewModel place = new PlaceViewModel(findPlace);
            return Ok(place);
        }

        // PUT: api/Places/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlace(int id, Place place)
        {
            var findPlace = _db.Places.Find(id);
            findPlace.PlaceVisitor = findPlace.PlaceVisitor + 1;
            _db.Entry(findPlace).State = EntityState.Modified;
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