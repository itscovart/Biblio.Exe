using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiblioExe.Models;
using Microsoft.AspNetCore.Authorization;


namespace BiblioExe.Controllers
{
    [Authorize(Roles = "3")]
    public class MateriasController : Controller
    {
        private readonly ContextoBD _context;

        public MateriasController(ContextoBD context)
        {
            _context = context;
        }

        // GET: Materias
        public async Task<IActionResult> Index()
        {
              return _context.Materias != null ? 
                          View(await _context.Materias.ToListAsync()) :
                          Problem("Entity set 'ContextoBD.Materias'  is null.");
        }

        // GET: Materias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Materias == null)
            {
                return NotFound();
            }

            var materias = await _context.Materias
                .FirstOrDefaultAsync(m => m.IDMateria == id);
            if (materias == null)
            {
                return NotFound();
            }

            return View(materias);
        }

        // GET: Materias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDMateria,Nombre,Semestre")] Materias materias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materias);
        }

        // GET: Materias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Materias == null)
            {
                return NotFound();
            }

            var materias = await _context.Materias.FindAsync(id);
            if (materias == null)
            {
                return NotFound();
            }
            return View(materias);
        }

        // POST: Materias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDMateria,Nombre,Semestre")] Materias materias)
        {
            if (id != materias.IDMateria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriasExists(materias.IDMateria))
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
            return View(materias);
        }

        // GET: Materias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Materias == null)
            {
                return NotFound();
            }

            var materias = await _context.Materias
                .FirstOrDefaultAsync(m => m.IDMateria == id);
            if (materias == null)
            {
                return NotFound();
            }

            return View(materias);
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Materias == null)
            {
                return Problem("Entity set 'ContextoBD.Materias'  is null.");
            }
            var materias = await _context.Materias.FindAsync(id);
            if (materias != null)
            {
                _context.Materias.Remove(materias);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriasExists(int id)
        {
          return (_context.Materias?.Any(e => e.IDMateria == id)).GetValueOrDefault();
        }
    }
}
