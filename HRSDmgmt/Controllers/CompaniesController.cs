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

namespace HRSDmgmt.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin, user")]
        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }


        // GET: Companies/Details/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        [Authorize(Roles = "admin, user")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Create([Bind("CompanyId,Name,NIP,Description,Address,Country,ContactPerson,Mobile,Email,Www,Active,Display")] Company company)
        {
            if (ModelState.IsValid)
            {
                if (!CompanyNameExists(company.Name) || !CompanyNipExists(company.NIP))
                {
                    _context.Add(company);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Taka firma już istnieje w bazie";
                    return View("Create");
                }
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,Name,NIP,Description,Address,Country,ContactPerson,Mobile,Email,Www,Active,Display")] Company company)
        {
            if (id != company.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.CompanyId))
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
            return View(company);
        }

        // GET: Companies/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Companies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Companies'  is null.");
            }
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(c => c.CompanyId == id);
        }

        private bool CompanyNameExists(string? name)
        {
            return (_context.Companies?.Any(c => c.Name == name)).GetValueOrDefault();
        }

        private bool CompanyNipExists(string? nip)
        {
            return (_context.Companies?.Any(c => c.NIP == nip)).GetValueOrDefault();
        }
    }
}