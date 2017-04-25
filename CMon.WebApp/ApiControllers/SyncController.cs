using CMon.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMon.WebApp.ApiControllers
{
    public class SyncController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //[HttpPost]
        //[Route("api/Sync")]
        public IHttpActionResult PostReadings(List<Reading> readings)
        {
            var user = db.Users.First();
            foreach (var r in readings)
            {
                r.Id = 0;
                r.Date = DateTime.Now;
                user.Readings.Add(r);
            }
            
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
