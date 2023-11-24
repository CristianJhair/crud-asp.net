using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class PersonalController : Controller
    {
        private readonly BdempresaContext _context;

        public PersonalController(BdempresaContext context)
        {
            _context = context;
        }

        //Lista de Personal
        public async Task<IActionResult> Index()
        {
            var bdempresaContext = _context.Personals.Include(p => p.CodigoCargoNavigation).Include(p => p.CodigoEmpresaNavigation);
            return View(await bdempresaContext.ToListAsync());
        }


        // GET: Create
        public IActionResult Create()
        {
            ViewData["CodigoCargo"] = new SelectList(_context.Cargos, "CodigoCargo", "CodigoCargo");
            ViewData["CodigoEmpresa"] = new SelectList(_context.Empresas, "CodigoEmpresa", "CodigoEmpresa");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoEmpleado,NumeroDocumento,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,FechaIngreso,CodigoEmpresa,CodigoCargo")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCargo"] = new SelectList(_context.Cargos, "CodigoCargo", "CodigoCargo", personal.CodigoCargo);
            ViewData["CodigoEmpresa"] = new SelectList(_context.Empresas, "CodigoEmpresa", "CodigoEmpresa", personal.CodigoEmpresa);
            return View(personal);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personals == null)
            {
                return NotFound();
            }

            var personal = await _context.Personals.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            ViewData["CodigoCargo"] = new SelectList(_context.Cargos, "CodigoCargo", "CodigoCargo", personal.CodigoCargo);
            ViewData["CodigoEmpresa"] = new SelectList(_context.Empresas, "CodigoEmpresa", "CodigoEmpresa", personal.CodigoEmpresa);
            return View(personal);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoEmpleado,NumeroDocumento,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,FechaIngreso,CodigoEmpresa,CodigoCargo")] Personal personal)
        {
            if (id != personal.CodigoEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalExists(personal.CodigoEmpleado))
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
            ViewData["CodigoCargo"] = new SelectList(_context.Cargos, "CodigoCargo", "CodigoCargo", personal.CodigoCargo);
            ViewData["CodigoEmpresa"] = new SelectList(_context.Empresas, "CodigoEmpresa", "CodigoEmpresa", personal.CodigoEmpresa);
            return View(personal);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personals == null)
            {
                return NotFound();
            }

            var personal = await _context.Personals
                .Include(p => p.CodigoCargoNavigation)
                .Include(p => p.CodigoEmpresaNavigation)
                .FirstOrDefaultAsync(m => m.CodigoEmpleado == id);
            if (personal == null)
            {
                return NotFound();
            }

            return View(personal);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personals == null)
            {
                return Problem("Entity set 'BdempresaContext.Personals'  is null.");
            }
            var personal = await _context.Personals.FindAsync(id);
            if (personal != null)
            {
                _context.Personals.Remove(personal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalExists(int id)
        {
          return (_context.Personals?.Any(e => e.CodigoEmpleado == id)).GetValueOrDefault();
        }
    }
}
