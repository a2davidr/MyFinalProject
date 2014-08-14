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
    public class SalesController : Controller
    {
        private DealershipDataEntities db = new DealershipDataEntities();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Customer).Include(s => s.Employee).Include(s => s.Vehicle);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName");
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "FirstName");
            ViewBag.VehicleID = new SelectList(db.Vehicle, "VehicleID", "Make");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleID,CustomerID,EmployeeID,SaleDate,SaleAmt,SaleCommission,ID")] Sales sales)
        {
            if (ModelState.IsValid)
            {
            try 
               { 
                db.Sales.Add(sales);
                db.SaveChanges();
                return RedirectToAction("Index");
                }
            catch (Exception e)
                {
                Console.WriteLine("Error Message = {0}", e.Message);
                }
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName", sales.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "FirstName", sales.EmployeeID);
            ViewBag.VehicleID = new SelectList(db.Vehicle, "VehicleID", "Make", sales.VehicleID);
            return View(sales);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName", sales.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "FirstName", sales.EmployeeID);
            ViewBag.VehicleID = new SelectList(db.Vehicle, "VehicleID", "Make", sales.VehicleID);
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleID,CustomerID,EmployeeID,SaleDate,SaleAmt,SaleCommission,ID")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "FirstName", sales.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employee, "EmployeeID", "FirstName", sales.EmployeeID);
            ViewBag.VehicleID = new SelectList(db.Vehicle, "VehicleID", "Make", sales.VehicleID);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales sales = db.Sales.Find(id);
            db.Sales.Remove(sales);
            db.SaveChanges();
            return RedirectToAction("Index");
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
