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
using WebServer2.Models.RepositoryModels;
using WebServer2.Repositories;

namespace WebServer2.Controllers
{
    public class BusBeaconsController : ApiController
    {
        private BeaconContext db = new BeaconContext();

        // GET: api/BusBeacons
        public IQueryable<BusBeacon> GetBusBeacons()
        {
            return db.BusBeacons;
        }

        // GET: api/BusBeacons/5
        [ResponseType(typeof(BusBeacon))]
        public async Task<IHttpActionResult> GetBusBeacon(int id)
        {
            BusBeacon busBeacon = await db.BusBeacons.FindAsync(id);
            if (busBeacon == null)
            {
                return NotFound();
            }

            return Ok(busBeacon);
        }

        // PUT: api/BusBeacons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBusBeacon(int id, BusBeacon busBeacon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != busBeacon.Id)
            {
                return BadRequest();
            }

            db.Entry(busBeacon).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusBeaconExists(id))
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

        // POST: api/BusBeacons
        [ResponseType(typeof(BusBeacon))]
        public async Task<IHttpActionResult> PostBusBeacon(BusBeacon busBeacon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BusBeacons.Add(busBeacon);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = busBeacon.Id }, busBeacon);
        }

        // DELETE: api/BusBeacons/5
        [ResponseType(typeof(BusBeacon))]
        public async Task<IHttpActionResult> DeleteBusBeacon(int id)
        {
            BusBeacon busBeacon = await db.BusBeacons.FindAsync(id);
            if (busBeacon == null)
            {
                return NotFound();
            }

            db.BusBeacons.Remove(busBeacon);
            await db.SaveChangesAsync();

            return Ok(busBeacon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusBeaconExists(int id)
        {
            return db.BusBeacons.Count(e => e.Id == id) > 0;
        }
    }
}