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
using DVDRental.Models.ViewModels;

namespace DVDRental.Controllers
{
    public class DVDCopiesController : Controller
    {
        private readonly AppDBContext _context;

        public DVDCopiesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: DVDCopies
        public async Task<IActionResult> Index(int searchString)
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            var dvdCopyList = _context.DVDCopies.ToList();
            //var userDetails = "HIVE MAGICK FUCKERY";
            //var userShopID = userDetails.ShopID;
            var dvdTitle = _context.DVDTitles.ToList();
            var member = _context.Members.ToList();
            var actor = _context.Actors.ToList();
            var loan = _context.Loans.ToList();

            var copyLastLoanDetails = (from dc in dvdCopyList
                                              join dt in dvdTitle
                                              on dc.DVDNumber equals dt.DVDNumber
                                              join l in loan
                                              on dc.CopyNumber equals l.CopyNumber
                                              join m in member 
                                              on l.MemberNumber equals m.MemberNumber  
                                              select new DVDCopiesIndexVM
                                              {
                                                MemberFirstName = m.MemberFirstName,
                                                MemberLastName = m.MemberLastName,
                                                DateOut = l.DateOut,
                                                DateDue = l.DateDue,
                                                DateReturned = l.DateRetured,
                                                DVDTitle = dt.Title
                                              }).ToList();

            if (!String.IsNullOrEmpty(searchString.ToString()))
            {
                var selectedCopy = dvdCopyList.Where(dc => dc.CopyNumber == searchString).ToList();

                var lastLoan = selectedCopy.Join(loan,
                                copy => copy.CopyNumber,
                                loan => loan.CopyNumber,
                                (selectedCopy, loan) => loan
                                ).OrderBy(l => l.DateOut).LastOrDefault();

                copyLastLoanDetails = (from dc in selectedCopy
                                        join dt in dvdTitle
                                        on dc.DVDNumber equals dt.DVDNumber
                                        join l in loan.Where(l => l.LoanNumber == lastLoan.LoanNumber)
                                        on dc.CopyNumber equals l.CopyNumber
                                        join m in member
                                        on l.MemberNumber equals m.MemberNumber
                                        select new DVDCopiesIndexVM
                                        {
                                            MemberFirstName = m.MemberFirstName,
                                            MemberLastName = m.MemberLastName,
                                            DateOut = l.DateOut,
                                            DateDue = l.DateDue,
                                            DateReturned = l.DateRetured,
                                            DVDTitle = dt.Title,
                                            CopyNumber = dc.CopyNumber
                                        }).ToList();
            }

            return View(copyLastLoanDetails);
        }

        // GET: DVDCopies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCopy = await _context.DVDCopies
                .Include(d => d.DVDTitle)
                .FirstOrDefaultAsync(m => m.CopyNumber == id);
            if (dVDCopy == null)
            {
                return NotFound();
            }

            return View(dVDCopy);
        }

        // GET: DVDCopies/Create
        public IActionResult Create()
        {
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitles, "DVDNumber", "Title");
            return View();
        }

        // POST: DVDCopies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CopyNumber,DatePurchased,DVDNumber")] DVDCopy dVDCopy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dVDCopy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitles, "DVDNumber", "Title", dVDCopy.DVDNumber);
            return View(dVDCopy);
        }

        // GET: DVDCopies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCopy = await _context.DVDCopies.FindAsync(id);
            if (dVDCopy == null)
            {
                return NotFound();
            }
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitles, "DVDNumber", "Title", dVDCopy.DVDNumber);
            return View(dVDCopy);
        }

        // POST: DVDCopies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CopyNumber,DatePurchased,DVDNumber")] DVDCopy dVDCopy)
        {
            if (id != dVDCopy.CopyNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dVDCopy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DVDCopyExists(dVDCopy.CopyNumber))
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
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitles, "DVDNumber", "Title", dVDCopy.DVDNumber);
            return View(dVDCopy);
        }

        // GET: DVDCopies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCopy = await _context.DVDCopies
                .Include(d => d.DVDTitle)
                .FirstOrDefaultAsync(m => m.CopyNumber == id);
            if (dVDCopy == null)
            {
                return NotFound();
            }

            return View(dVDCopy);
        }

        // POST: DVDCopies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dVDCopy = await _context.DVDCopies.FindAsync(id);
            _context.DVDCopies.Remove(dVDCopy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DVDCopyExists(int id)
        {
            return _context.DVDCopies.Any(e => e.CopyNumber == id);
        }
    }
}
