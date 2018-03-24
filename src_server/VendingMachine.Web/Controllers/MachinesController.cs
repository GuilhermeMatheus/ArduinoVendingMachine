using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VendingMachine.Core.Model;
using VendingMachine.EF;

namespace VendingMachine.Web.Controllers
{
    public class MachinesController : Controller
    {
        private readonly VendingMachineDbContext _db;

        public MachinesController(VendingMachineDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // GET: Machines
        public async Task<ActionResult> Index()
        {
            return View(await _db.Machines.ToListAsync());
        }

        // GET: Machines/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = await _db.Machines.FindAsync(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // GET: Machines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Alias,IsActivated,Address")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                _db.Machines.Add(machine);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(machine);
        }

        // GET: Machines/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = await _db.Machines.FindAsync(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Alias,IsActivated,Address")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(machine).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(machine);
        }

        // GET: Machines/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = await _db.Machines.FindAsync(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Machine machine = await _db.Machines.FindAsync(id);
            _db.Machines.Remove(machine);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
