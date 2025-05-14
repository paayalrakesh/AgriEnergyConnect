using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;

namespace AgriEnergyConnect.Controllers
{
    public class FarmerProfilesController : Controller
    {
        private readonly AppDbContext _context;

        public FarmerProfilesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FarmerProfiles
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.FarmerProfiles.Include(f => f.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: FarmerProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmerProfile = await _context.FarmerProfiles
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FarmerProfileId == id);
            if (farmerProfile == null)
            {
                return NotFound();
            }

            return View(farmerProfile);
        }

        // GET: FarmerProfiles/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Password");
            return View();
        }

        // POST: FarmerProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmerProfileId,FullName,Region,ContactNumber,UserId")] FarmerProfile farmerProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmerProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Password", farmerProfile.UserId);
            return View(farmerProfile);
        }

        // GET: FarmerProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmerProfile = await _context.FarmerProfiles.FindAsync(id);
            if (farmerProfile == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Password", farmerProfile.UserId);
            return View(farmerProfile);
        }

        // POST: FarmerProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FarmerProfileId,FullName,Region,ContactNumber,UserId")] FarmerProfile farmerProfile)
        {
            if (id != farmerProfile.FarmerProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmerProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmerProfileExists(farmerProfile.FarmerProfileId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Password", farmerProfile.UserId);
            return View(farmerProfile);
        }

        // GET: FarmerProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmerProfile = await _context.FarmerProfiles
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FarmerProfileId == id);
            if (farmerProfile == null)
            {
                return NotFound();
            }

            return View(farmerProfile);
        }

        // POST: FarmerProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmerProfile = await _context.FarmerProfiles.FindAsync(id);
            if (farmerProfile != null)
            {
                _context.FarmerProfiles.Remove(farmerProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmerProfileExists(int id)
        {
            return _context.FarmerProfiles.Any(e => e.FarmerProfileId == id);
        }
    }
}
