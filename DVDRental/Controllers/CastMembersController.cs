#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DVDRental.Areas.Identity.Data;
using DVDRental.Models;

namespace DVDRental.Controllers
{
    public class CastMembersController : Controller
    {
        private readonly AppDBContext _context;

        public CastMembersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: CastMembers
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.CastMembers.Include(c => c.Actor).Include(c => c.DVDTitle);
            return View(await appDBContext.ToListAsync());
        }

        // GET: CastMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var castMember = await _context.CastMembers
                .Include(c => c.Actor)
                .Include(c => c.DVDTitle)
                .FirstOrDefaultAsync(m => m.ActorId == id);
            if (castMember == null)
            {
                return NotFound();
            }

            return View(castMember);
        }

        // GET: CastMembers/Create
        public IActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorFirstName");
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitles, "DVDNumber", "Title");
            return View();
        }

        // POST: CastMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorId,DVDNumber")] CastMember castMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(castMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorFirstName", castMember.ActorId);
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitles, "DVDNumber", "Title", castMember.DVDNumber);
            return View(castMember);
        }

        // GET: CastMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var castMember = await _context.CastMembers.FindAsync(id);
            if (castMember == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorFirstName", castMember.ActorId);
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitles, "DVDNumber", "Title", castMember.DVDNumber);
            return View(castMember);
        }

        // POST: CastMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActorId,DVDNumber")] CastMember castMember)
        {
            if (id != castMember.ActorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(castMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CastMemberExists(castMember.ActorId))
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
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorFirstName", castMember.ActorId);
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitles, "DVDNumber", "Title", castMember.DVDNumber);
            return View(castMember);
        }

        // GET: CastMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var castMember = await _context.CastMembers
                .Include(c => c.Actor)
                .Include(c => c.DVDTitle)
                .FirstOrDefaultAsync(m => m.ActorId == id);
            if (castMember == null)
            {
                return NotFound();
            }

            return View(castMember);
        }

        // POST: CastMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var castMember = await _context.CastMembers.FindAsync(id);
            _context.CastMembers.Remove(castMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CastMemberExists(int id)
        {
            return _context.CastMembers.Any(e => e.ActorId == id);
        }
    }
}
