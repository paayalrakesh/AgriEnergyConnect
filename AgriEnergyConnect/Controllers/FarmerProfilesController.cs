using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgriEnergyConnect.Controllers
{
    public class FarmerProfilesController : Controller
    {
        private readonly AppDbContext _context;

        public FarmerProfilesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.FarmerProfiles.Include(f => f.User).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var farmerProfile = await _context.FarmerProfiles
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FarmerProfileId == id);
            return farmerProfile == null ? NotFound() : View(farmerProfile);
        }
        // GET: FarmerProfiles/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return RedirectToAction("Login", "Account");

            // Populate dropdown with Farmer users
            var farmers = _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role.RoleName == "Farmer")
                .Select(u => new { u.UserId, u.Username })
                .ToList();

            ViewData["UserId"] = new SelectList(farmers, "UserId", "Username");

            return View();
        }

        // POST: FarmerProfiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmerProfileId,FullName,Region,ContactNumber,UserId")] FarmerProfile farmerProfile)
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Add(farmerProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If we reach this point, we need to refill the dropdown
            var farmers = _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role.RoleName == "Farmer")
                .Select(u => new { u.UserId, u.Username })
                .ToList();

            ViewData["UserId"] = new SelectList(farmers, "UserId", "Username", farmerProfile.UserId);

            return View(farmerProfile);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var farmerProfile = await _context.FarmerProfiles.FindAsync(id);
            return farmerProfile == null ? NotFound() : View(farmerProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FarmerProfile farmerProfile)
        {
            if (id != farmerProfile.FarmerProfileId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmerProfile);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.FarmerProfiles.Any(e => e.FarmerProfileId == id)) return NotFound();
                    throw;
                }
            }

            return View(farmerProfile);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var farmerProfile = await _context.FarmerProfiles
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FarmerProfileId == id);
            return farmerProfile == null ? NotFound() : View(farmerProfile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmerProfile = await _context.FarmerProfiles.FindAsync(id);
            if (farmerProfile != null) _context.FarmerProfiles.Remove(farmerProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
