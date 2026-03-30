using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DgsTakipSistemi.Data;
using DgsTakipSistemi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DgsTakipSistemi.Controllers
{
    [Authorize]
    public class CalismaPlanlariController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalismaPlanlariController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CalismaPlanlari
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var planlar = await _context.CalismaPlanlari.Where(p => p.KullaniciId == userId).ToListAsync();
            return View(planlar);
        }

        // GET: CalismaPlanlari/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calismaPlani = await _context.CalismaPlanlari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calismaPlani == null)
            {
                return NotFound();
            }

            return View(calismaPlani);
        }

        // GET: CalismaPlanlari/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CalismaPlanlari/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GorevAdi,Gun,TamamlandiMi")] CalismaPlani calismaPlani)
        {
           
            calismaPlani.KullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ModelState.Remove("KullaniciId");

            if (ModelState.IsValid)
            {
                _context.Add(calismaPlani);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calismaPlani);
        }

        // GET: CalismaPlanlari/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calismaPlani = await _context.CalismaPlanlari.FindAsync(id);
            if (calismaPlani == null)
            {
                return NotFound();
            }
            return View(calismaPlani);
        }

        // POST: CalismaPlanlari/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KullaniciId,GorevAdi,Gun,TamamlandiMi")] CalismaPlani calismaPlani)
        {
            if (id != calismaPlani.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calismaPlani);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalismaPlaniExists(calismaPlani.Id))
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
            return View(calismaPlani);
        }

        // GET: CalismaPlanlari/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calismaPlani = await _context.CalismaPlanlari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calismaPlani == null)
            {
                return NotFound();
            }

            return View(calismaPlani);
        }

        // POST: CalismaPlanlari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calismaPlani = await _context.CalismaPlanlari.FindAsync(id);
            if (calismaPlani != null)
            {
                _context.CalismaPlanlari.Remove(calismaPlani);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalismaPlaniExists(int id)
        {
            return _context.CalismaPlanlari.Any(e => e.Id == id);
        }
    }
}
