using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MinhaDemoMvc.Data;
using MinhaDemoMvc.Models;

namespace MinhaDemoMvc.Controllers
{
    public class testeScaffoldsController : Controller
    {
        private readonly MinhaDemoMvcContext _context;

        public testeScaffoldsController(MinhaDemoMvcContext context)
        {
            _context = context;
        }

        // GET: testeScaffolds
        public async Task<IActionResult> Index()
        {
            return View(await _context.testeScaffold.ToListAsync());
        }

        // GET: testeScaffolds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testeScaffold = await _context.testeScaffold
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testeScaffold == null)
            {
                return NotFound();
            }

            return View(testeScaffold);
        }

        // GET: testeScaffolds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: testeScaffolds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] testeScaffold testeScaffold)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testeScaffold);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testeScaffold);
        }

        // GET: testeScaffolds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testeScaffold = await _context.testeScaffold.FindAsync(id);
            if (testeScaffold == null)
            {
                return NotFound();
            }
            return View(testeScaffold);
        }

        // POST: testeScaffolds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] testeScaffold testeScaffold)
        {
            if (id != testeScaffold.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testeScaffold);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!testeScaffoldExists(testeScaffold.Id))
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
            return View(testeScaffold);
        }

        // GET: testeScaffolds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testeScaffold = await _context.testeScaffold
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testeScaffold == null)
            {
                return NotFound();
            }

            return View(testeScaffold);
        }

        // POST: testeScaffolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testeScaffold = await _context.testeScaffold.FindAsync(id);
            _context.testeScaffold.Remove(testeScaffold);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool testeScaffoldExists(int id)
        {
            return _context.testeScaffold.Any(e => e.Id == id);
        }
    }
}
