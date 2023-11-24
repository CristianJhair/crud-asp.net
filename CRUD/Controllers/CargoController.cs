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
    public class CargoController : Controller
    {
        private readonly BdempresaContext _context;

        public CargoController(BdempresaContext context)
        {
            _context = context;
        }

        // Lista cargos
        public async Task<IActionResult> Index()
        {
              return _context.Cargos != null ? 
                          View(await _context.Cargos.ToListAsync()) :
                          Problem("Entity set 'BdempresaContext.Cargos'  is null.");
        }


        // GET:Create
        public IActionResult Create()
        {
            return View();
        }

        // POST:Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoCargo,Descripcion,FechaCreacion,FlagModificado")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargo);  //objeto en estado "agregado" pero aún no se ha guardado en la base de datos.
                await _context.SaveChangesAsync(); //inserción real
                return RedirectToAction(nameof(Index)); 
            }
            return View(cargo);
        }

        // GET:Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cargos == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos.FindAsync(id); //encontrar objeto cargo según id
            if (cargo == null)
            {
                return NotFound();
            }
            return View(cargo);
        }

        // POST:Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoCargo,Descripcion,FechaCreacion,FlagModificado")] Cargo cargo)
        {
            if (id != cargo.CodigoCargo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargo); //actualiza el contexto de bbdd
                    await _context.SaveChangesAsync(); //guarda los cambios.
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoExists(cargo.CodigoCargo))
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
            return View(cargo);
        }

        // GET:Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cargos == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos
                .FirstOrDefaultAsync(m => m.CodigoCargo == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // POST:Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cargos == null)
            {
                return Problem("Entity set 'BdempresaContext.Cargos'  is null.");
            }
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo != null)
            {
                _context.Cargos.Remove(cargo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CargoExists(int id)
        {
          return (_context.Cargos?.Any(e => e.CodigoCargo == id)).GetValueOrDefault();
        }
    }
}
