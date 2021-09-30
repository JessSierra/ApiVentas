using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiVentas.Models;

namespace ApiVentas.Controllers
{
    public class ventasController : ApiController
    {
        private PuntoVentaEntities5 db = new PuntoVentaEntities5();

        // GET: api/ventas
        public IQueryable<ventas> Getventas()
        {
            return db.ventas;
        }

        // GET: api/ventas/5
        [ResponseType(typeof(ventas))]
        public IHttpActionResult Getventas(int id)
        {
            ventas ventas = db.ventas.Find(id);
            if (ventas == null)
            {
                return NotFound();
            }

            return Ok(ventas);
        }

        // PUT: api/ventas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putventas(int id, ventas ventas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ventas.IdVenta)
            {
                return BadRequest();
            }

            db.Entry(ventas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ventasExists(id))
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

        // POST: api/ventas
        [ResponseType(typeof(ventas))]
        public IHttpActionResult Postventas(ventas ventas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ventas.Add(ventas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ventas.IdVenta }, ventas);
        }

        // DELETE: api/ventas/5
        [ResponseType(typeof(ventas))]
        public IHttpActionResult Deleteventas(int id)
        {
            ventas ventas = db.ventas.Find(id);
            if (ventas == null)
            {
                return NotFound();
            }

            db.ventas.Remove(ventas);
            db.SaveChanges();

            return Ok(ventas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ventasExists(int id)
        {
            return db.ventas.Count(e => e.IdVenta == id) > 0;
        }
    }
}