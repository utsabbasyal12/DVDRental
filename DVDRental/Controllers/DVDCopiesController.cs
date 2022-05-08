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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
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
                                                DVDTitle = dt.Title,
                                                CopyNumber = dc.CopyNumber
                                              }).ToList();

            if (!String.IsNullOrEmpty(searchString.ToString()))
            {
                var selectedCopy = dvdCopyList.Where(dc => dc.CopyNumber == searchString).ToList();

                var lastLoan = selectedCopy.Join(loan,
                                copy => copy.CopyNumber,
                                loan => loan.CopyNumber,
                                (selectedCopy, loan) => loan
                                ).OrderBy(l => l.DateOut).LastOrDefault();
                if (lastLoan == null)
                {
                    return View(copyLastLoanDetails);
                }
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

        [Authorize]
        public async Task<IActionResult> LoanedCopies(string orderByOption)
        {
            var dvdCopies = _context.DVDCopies.ToList();
            var dvdTitles = _context.DVDTitles.ToList();
            var loans = _context.Loans.ToList();
            var members = _context.Members.ToList();
            IEnumerable<LoanedCopiesVM> LoanedCopyDetails = null;

            orderByOption = orderByOption ?? "orderByTitle";

            if (orderByOption == "orderByDate")
            {
                //LoanedCopyDetails = LoanedCopyDetails.OrderBy(x => x.loanedCopiesVMs.First().DateOut?.Date).ThenBy(x => x.loanedCopiesVMs.First().DVDTitle);
                LoanedCopyDetails = (from dc in dvdCopies
                 join l in loans.Where(l => l.DateRetured == null)
                 on dc.CopyNumber equals l.CopyNumber
                 join dt in dvdTitles
                 on dc.DVDNumber equals dt.DVDNumber
                 join m in members
                 on l.MemberNumber equals m.MemberNumber
                 select new LoanedCopiesVM
                 {
                     DVDTitle = dt.Title,
                     DateOut = l.DateOut,
                     CopyNumber = dc.CopyNumber,
                     MemberFirstName = m.MemberFirstName,
                     MemberLastName = m.MemberLastName,
                 }).GroupBy(x => x.DateOut?.Date)
                                     .Select(g => new LoanedCopiesVM
                                     {
                                         CopyCount = g.Count(),
                                         loanedCopiesVMs = g.OrderBy(x => x.DateOut).ToList()
                                     }).OrderBy(x => x.loanedCopiesVMs.First().DateOut);

            }

            else if (orderByOption == "orderByTitle")
            {
                //LoanedCopyDetails = LoanedCopyDetails.OrderBy(x => x.loanedCopiesVMs.First().DVDTitle).ThenBy(x => x.loanedCopiesVMs.First().DateOut?.Date);
                LoanedCopyDetails = (from dc in dvdCopies
                 join l in loans.Where(l => l.DateRetured == null)
                 on dc.CopyNumber equals l.CopyNumber
                 join dt in dvdTitles
                 on dc.DVDNumber equals dt.DVDNumber
                 join m in members
                 on l.MemberNumber equals m.MemberNumber
                 select new LoanedCopiesVM
                 {
                     DVDTitle = dt.Title,
                     DateOut = l.DateOut,
                     CopyNumber = dc.CopyNumber,
                     MemberFirstName = m.MemberFirstName,
                     MemberLastName = m.MemberLastName,
                 }).GroupBy(x => x.DateOut?.Date)
                                     .Select(g => new LoanedCopiesVM
                                     {
                                         CopyCount = g.Count(),
                                         loanedCopiesVMs = g.OrderBy(x => x.DVDTitle).ToList()
                                     }).OrderBy(x => x.loanedCopiesVMs.First().DVDTitle);

            }



            return View(LoanedCopyDetails.ToList());
        }

        [Authorize]
        public async Task<IActionResult> UnloanedCopies()
        {
            var dvdCopies = _context.DVDCopies.ToList();
            var dvdTitles = _context.DVDTitles.ToList();
            var loans = _context.Loans.ToList();

            var loanedTitlesWithinAMonth = from dc in dvdCopies
                                           join l in loans.Where(l => (DateTime.Now.Subtract(l.DateOut).TotalDays <= 31))
                                           on dc.CopyNumber equals l.CopyNumber
                                           join dt in dvdTitles
                                           on dc.DVDNumber equals dt.DVDNumber
                                           select dt;

            var unloandedTitlesWithinAMonth = dvdTitles.Except(loanedTitlesWithinAMonth);


            return View(unloandedTitlesWithinAMonth);
        }







        // GET: DVDCopies/Details/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dVDCopy = await _context.DVDCopies.FindAsync(id);
            _context.DVDCopies.Remove(dVDCopy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool DVDCopyExists(int id)
        {
            return _context.DVDCopies.Any(e => e.CopyNumber == id);
        }
    }
}
