using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PROJECT_MVC_API.API
{
    public class APIController : ApiController
    {
        ProjectDBEntities db = new ProjectDBEntities();
        // GET: api/API
        [HttpGet]
        [Route("api/API/getwebapitabs")]
        [ResponseType(typeof(BookTab))]
        public IHttpActionResult Get()
        {
            return Ok(db.BookTabs.ToList());
        }
        // GET: api/API/5
        [HttpGet]
        [Route("api/API/getwebapitab/{id}")]
        [ResponseType(typeof(BookTab))]
        public IHttpActionResult Get(int id)
        {
            BookTab bookTab = db.BookTabs.Find(id);
            if (bookTab == null)
            {
                return NotFound();
            }
            return Ok(bookTab);
        }

        // POST: api/API
        [HttpPost]
        [Route("api/API/postwebapitab")]
        [ResponseType(typeof(BookTab))]
        public IHttpActionResult Post(BookTab bookTab)
        {
            if (ModelState.IsValid)
            {
                db.BookTabs.Add(bookTab);
                db.SaveChanges();
                return Ok(200);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/API/5
        [HttpPut]
        [Route("api/API/putwebapitab/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, BookTab bookTab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != bookTab.BookId)
            {
                return BadRequest();
            }
            db.Entry(bookTab).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/API/5
        [HttpDelete]
        [Route("api/API/deletewebapitab/{id}")]
        [ResponseType(typeof(BookTab))]
        public IHttpActionResult Delete(int id)
        {
            BookTab webAPiTable = db.BookTabs.Find(id);
            if (webAPiTable == null)
            {
                return NotFound();
            }
            db.BookTabs.Remove(webAPiTable);
            db.SaveChanges();
            return Ok(webAPiTable);
        }
    }
}
