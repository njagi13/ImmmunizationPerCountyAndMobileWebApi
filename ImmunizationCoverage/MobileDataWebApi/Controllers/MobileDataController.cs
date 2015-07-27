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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using MobileDataWebApi.Models;

namespace MobileDataWebApi.Controllers
{
    [EnableCors(origins:"http://immunizationcoverage.azurewebsites.net", headers: "*", methods: "*")]
    public class MobileDataController : ApiController
    {
        private MobileDataWebApiContext db = new MobileDataWebApiContext();

        // GET: api/MobileData
        public IList<MobileData> GetMobileDatas()
        {
            return db.MobileDatas.ToList();
        }

        // GET: api/MobileData/5
        [ResponseType(typeof(MobileData))]
        public async Task<IHttpActionResult> GetMobileData(Guid id)
        {
            MobileData mobileData = await db.MobileDatas.FindAsync(id);
            if (mobileData == null)
            {
                return NotFound();
            }

            return Ok(mobileData);
        }

        // PUT: api/MobileData/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMobileData(Guid id, MobileData mobileData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mobileData.Id)
            {
                return BadRequest();
            }

            db.Entry(mobileData).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MobileDataExists(id))
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

        // POST: api/MobileData
        [ResponseType(typeof(MobileData))]
        public async Task<IHttpActionResult> PostMobileData(MobileData mobileData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MobileDatas.Add(mobileData);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MobileDataExists(mobileData.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mobileData.Id }, mobileData);
        }

        // DELETE: api/MobileData/5
        [ResponseType(typeof(MobileData))]
        public async Task<IHttpActionResult> DeleteMobileData(Guid id)
        {
            MobileData mobileData = await db.MobileDatas.FindAsync(id);
            if (mobileData == null)
            {
                return NotFound();
            }

            db.MobileDatas.Remove(mobileData);
            await db.SaveChangesAsync();

            return Ok(mobileData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MobileDataExists(Guid id)
        {
            return db.MobileDatas.Count(e => e.Id == id) > 0;
        }
    }
}