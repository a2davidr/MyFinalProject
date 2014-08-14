using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DealershipApp.Models;

namespace DealershipApp.Controllers
{
    public class VehicleController : Controller
    {
        private DealershipDataEntities db = new DealershipDataEntities();

        // GET: Vehicle
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.MakeSortParm = String.IsNullOrEmpty(sortOrder) ? "make" : "";
            ViewBag.ModelSortParm = sortOrder == "model" ? "model" : "model";
            ViewBag.YearSortParm = sortOrder == "year" ? "year" : "year";
            var vehicle = from v in db.Vehicle
                          select v;
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicle = vehicle.Where(v => v.Make.ToUpper().Contains(searchString.ToUpper())
                                     || v.Model.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "make":
                    vehicle = vehicle.OrderBy(v => v.Make);
                    break;
                case "model":
                    vehicle = vehicle.OrderBy(v => v.Model);
                    break;
                case "year":
                    vehicle = vehicle.OrderByDescending(v => v.Year);
                    break;
                default:
                    vehicle = vehicle.OrderBy(v => v.Make);
                    break;
            }
            return View(vehicle.ToList());
        }
 /*       public ActionResult Index()
        {
            return View(db.Vehicle.ToList());
        }
        */
        // GET: Vehicle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleID,Make,Model,Year,Miles,Color,AcquiredDate,AcquiredAmt,SaleDate,SaleAmt,VehicleType")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicle.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // GET: Vehicle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleID,Make,Model,Year,Miles,Color,AcquiredDate,AcquiredAmt,SaleDate,SaleAmt,VehicleType")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicle.Find(id);
            db.Vehicle.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sell(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Create", "Sales", new { id });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
