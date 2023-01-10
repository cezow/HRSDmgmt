using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRSDmgmt.Data;
using HRSDmgmt.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Threading;

namespace HRSDmgmt.Controllers
{
    public class OffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Offers
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IndexForAdmin()
        {
            var applicationDbContext = _context.Offers.Include(o => o.Company).Where(o => o.Active == false);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Offers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Offers.Include(o => o.Company).Where(o => o.Active == true);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Offers/Details/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Offers == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .Include(o => o.Company)
                .FirstOrDefaultAsync(m => m.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }

            var candidates = await _context.Candidate
                .Where(c => c.OfferId == id)
                .ToListAsync();

            ViewBag.Candidates = candidates;

            return View(offer);
        }

        // GET: Offers/Create
      
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "Name");
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferId,Name,Description,Vacancy,AddDate,StartDate,EndDate,Active,Display,CompanyId")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                ViewBag.sent = "Zlecenie zostało wysłane";
                _context.Add(offer);
                await _context.SaveChangesAsync();
                Thread.Sleep(4000);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies.Where(c => c.Active == true), "CompanyId", "Name", offer.CompanyId);
            
            return View(offer);
        }

        // GET: Offers/Edit/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Offers == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies.Where(c => c.Active == true), "CompanyId", "Name", offer.CompanyId);

            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Edit(int id, [Bind("OfferId,Name,Description,Vacancy,AddDate,StartDate,EndDate,Active,Display,CompanyId")] Offer offer)
        {
            if (id != offer.OfferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.OfferId))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies.Where(c => c.Active == true), "CompanyId", "Name", offer.CompanyId);
            return View(offer);
        }

        // GET: Offers/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Offers == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .Include(o => o.Company)
                .FirstOrDefaultAsync(m => m.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Offers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Offers'  is null.");
            }
            var offer = await _context.Offers.FindAsync(id);
            if (offer != null)
            {
                _context.Offers.Remove(offer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfferExists(int id)
        {
          return _context.Offers.Any(e => e.OfferId == id);
        }
    }
}
