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


        /*
Title: Entity Framework Core - Relationships and Navigation Properties
Author: Microsoft Docs
Date: 2023
Code version: EF Core 6
Availability: https://learn.microsoft.com/en-us/ef/core/modeling/relationships/
*/

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


        /*
Title: Model Binding and Validation in ASP.NET Core
Author: Microsoft Docs
Date: 2023
Code version: ASP.NET Core 6
Availability: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-6.0
*/

        // POST: FarmerProfiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmerProfileId,FullName,Region,ContactNumber,UserId")] FarmerProfile farmerProfile)
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return RedirectToAction("Login", "Account");

                _context.Add(farmerProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
      

            // Repopulate ViewBag if model state is invalid
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
