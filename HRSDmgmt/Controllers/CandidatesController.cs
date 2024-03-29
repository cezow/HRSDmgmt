﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRSDmgmt.Data;
using HRSDmgmt.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HRSDmgmt.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Candidates
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Candidate.Include(c => c.Employee).Include(c => c.Offer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Candidates/Details/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Candidate == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidate
                .Include(c => c.Employee)
                .Include(c => c.Offer)
                .FirstOrDefaultAsync(m => m.CandidateId == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // GET: Candidates/Create
        [Authorize(Roles = "admin, user")]
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["OfferId"] = new SelectList(_context.Offers.Where(o => o.Active == true), "OfferId", "OfferId");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Create([Bind("CandidateId,OfferId,EmployeeId")] Candidate candidate)
        {
            if (ModelState.IsValid && !SameCandidateExists(candidate))
            {
                _context.Add(candidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.error = "Ten kandydat juz jest przypisany do tej oferty pracy!";
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", candidate.EmployeeId);
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", candidate.OfferId);
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Candidate == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidate.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", candidate.EmployeeId);
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", candidate.OfferId);
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Edit(int id, [Bind("CandidateId,OfferId,EmployeeId")] Candidate candidate)
        {
            if (id != candidate.CandidateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExists(candidate.CandidateId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", candidate.EmployeeId);
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", candidate.OfferId);
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Candidate == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidate
                .Include(c => c.Employee)
                .Include(c => c.Offer)
                .FirstOrDefaultAsync(m => m.CandidateId == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Candidate == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Candidate'  is null.");
            }
            var candidate = await _context.Candidate.FindAsync(id);
            if (candidate != null)
            {
                _context.Candidate.Remove(candidate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExists(int id)
        {
          return _context.Candidate.Any(e => e.CandidateId == id);
        }

        private bool SameCandidateExists(Candidate newCandidate)
        {
            return _context.Candidate.Any(e => e.OfferId == newCandidate.OfferId && e.EmployeeId == newCandidate.EmployeeId);
        }
    }
}
