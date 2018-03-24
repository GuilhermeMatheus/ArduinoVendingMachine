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
    public class ClientCardsController : Controller
    {
        private readonly VendingMachineDbContext _db;

        public ClientCardsController(VendingMachineDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // GET: ClientCards
        public async Task<ActionResult> Index()
        {
            return View(await _db.ClientCards.ToListAsync());
        }

        // GET: ClientCards/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientCard clientCard = await _db.ClientCards.FindAsync(id);
            if (clientCard == null)
            {
                return HttpNotFound();
            }
            return View(clientCard);
        }

        // GET: ClientCards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Rfid,Alias,Credit")] ClientCard clientCard)
        {
            if (ModelState.IsValid)
            {
                _db.ClientCards.Add(clientCard);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(clientCard);
        }

        // GET: ClientCards/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientCard clientCard = await _db.ClientCards.FindAsync(id);
            if (clientCard == null)
            {
                return HttpNotFound();
            }
            return View(clientCard);
        }

        // POST: ClientCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Rfid,Alias,Credit")] ClientCard clientCard)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(clientCard).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clientCard);
        }

        // GET: ClientCards/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientCard clientCard = await _db.ClientCards.FindAsync(id);
            if (clientCard == null)
            {
                return HttpNotFound();
            }
            return View(clientCard);
        }

        // POST: ClientCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ClientCard clientCard = await _db.ClientCards.FindAsync(id);
            _db.ClientCards.Remove(clientCard);
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
