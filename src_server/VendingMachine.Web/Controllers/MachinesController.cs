using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Core.Model;
using VendingMachine.Core.Repository;
using VendingMachine.Core.Services;
using VendingMachine.EF;

namespace VendingMachine.Web.Controllers
{
    public class MachinesController : Controller
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IVendingMachineControlService _vendingMachineControlService;

        public MachinesController(IMachineRepository machineRepository, IVendingMachineControlService vendingMachineControlService)
        {
            _machineRepository = machineRepository;
            _vendingMachineControlService = vendingMachineControlService;
        }

        // GET: Machines
        public async Task<IActionResult> Index()
        {
            return View(await _machineRepository.FetchAllAsync());
        }

        // GET: Machines/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var machine = await _machineRepository.GetAsync(id);

            if (machine == null)
                return NotFound();

            return View(machine);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePriceTable(int id)
        {
            var machine = await _machineRepository.GetAsync(id);

            _vendingMachineControlService.CreateUpdateMachinePriceTableJob(machine);

            return RedirectToAction("Details", new { id });
        }

        // GET: Machines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Machines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Alias,IsActivated,Address,IPEndPoint")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                _machineRepository.Add(machine);
                await _machineRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(machine);
        }

        // GET: Machines/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var machine = await _machineRepository.GetAsync(id);

            if (machine == null)
                return NotFound();

            return View(machine);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Alias,IsActivated,Address,IPEndPoint")] Machine machine)
        {
            if (id != machine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _machineRepository.Update(machine);
                    await _machineRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineExists(machine.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(machine);
        }

        // GET: Machines/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _machineRepository.GetAsync(id);

            if (machine == null)
                return NotFound();

            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machine = await _machineRepository.GetAsync(id);
            _machineRepository.Delete(machine);
            await _machineRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachineExists(int id)
        {
            return _machineRepository.Get(id) != null;
        }
    }
}
