using CMon.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CMon.WebApp.Controllers
{
    public class ChartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Day()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Day(DateTime day)
        {
            var readings = db.Readings
                .Where(r => DbFunctions.DiffDays(r.Date, day) == 0)
                .OrderBy(r => r.Date)
                .ToList()
                .Select(r => new { Date = r.Date.TimeOfDay.ToString(), r.Power })
                .ToList();


            var chart = new Chart(600, 400)
                .AddSeries(chartType: "Area",
                xValue: readings, yValues: readings, xField: "Date", yFields: "Power")
                .Write();

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}