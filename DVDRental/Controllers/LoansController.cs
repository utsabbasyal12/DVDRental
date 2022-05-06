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
    public class LoansController : Controller
    {
        private readonly AppDBContext _context;

        public LoansController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Loans.Include(l => l.DVDCopy).Include(l => l.LoanType).Include(l => l.Member);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.DVDCopy)
                .Include(l => l.LoanType)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(m => m.LoanNumber == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create

        public IActionResult Create()
        {
            ViewData["CopyNumber"] = new SelectList(_context.DVDCopies, "CopyNumber", "CopyNumber");
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypes, "LoanTypeNumber", "LoanTypeNumber");
            ViewData["MemberNumber"] = new SelectList(_context.Members, "MemberNumber", "MemberFirstName");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanNumber,DateOut,DateDue,DateRetured,LoanTypeNumber,CopyNumber,MemberNumber")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                var member = _context.Members.Where(x => x.MemberNumber == loan.MemberNumber).ToList();
                var memberDOB = member[0].MemberDOB;
                int age = 0;
                age = DateTime.Now.Subtract(memberDOB).Days / 365;
                //var shopList = _context.Shop.ToList();
                var chargeFlag = false;
                var memberList = _context.Members;
                var dvdCopyList = _context.DVDCopies;
                var dvdTitleList = _context.DVDTitles;
                var studio = _context.Studios;
                var producer = _context.Producers;
                var loans = _context.Loans;
                var dvdCategory = _context.DVDCategory;
                var selectedCopyNumber = loan.CopyNumber;
                var selectedCopy = dvdCopyList.Where(x => x.CopyNumber == selectedCopyNumber);
                var selectedMember = memberList.Where(x => x.MemberNumber == loan.MemberNumber);
                var curentLoanCount = (from l in loans.Where(l => l.DateRetured == null)
                                       join m in selectedMember
                                    on l.MemberNumber equals m.MemberNumber
                                       group l by new { l.MemberNumber } into g
                                       select new CountVM
                                       {
                                           Count = g.Count()
                                       }).FirstOrDefault();

                var currentLoanCountInt = (curentLoanCount == null) ? 0 : curentLoanCount.Count ;

                var maxLoans = (from m in selectedMember
                                join mc in _context.MembershipCategories
                                on m.MembershipCategoryNumber equals mc.MembershipCategoryNumber
                                select new CountVM
                                {
                                    Count = mc.MembershipCategoryTotalLoans
                                }).FirstOrDefault();
                var maxLoanInt = maxLoans.Count;

                var ageRestricted = (from sc in selectedCopy
                                     join dt in dvdTitleList
                                     on sc.DVDNumber equals dt.DVDNumber
                                     join dcat in dvdCategory
                                     on dt.CategoryNumber equals dcat.CategoryNumber
                                     select new
                                     {
                                         dcat.AgeRestricted

                                     }).ToList();
                var restricted = ageRestricted[0].AgeRestricted.ToString();
                var selectedDvdTitle = dvdCopyList.Where(dc => dc.CopyNumber == loan.CopyNumber)
                        .Join(dvdTitleList,
                        copy => copy.DVDNumber,
                        title => title.DVDNumber,
                        (dvdCopyList, title) => title).FirstOrDefault();
                var standardCharge = selectedDvdTitle.StandardCharge;

                //charge calculation
                var loanDuration = (loan.DateDue - loan.DateOut).TotalDays;
                var charge = loanDuration * (Decimal.ToDouble(standardCharge));
                if (age >= 18 && (currentLoanCountInt < maxLoanInt))
                {
                    _context.Add(loan);
                    await _context.SaveChangesAsync();
                    chargeFlag = true;
                    //return RedirectToAction(nameof(Index));
                }
                else if (age < 18 && restricted != "NotRestricted" && (currentLoanCountInt < maxLoanInt))
                {
                    _context.Add(loan);
                    await _context.SaveChangesAsync();
                    chargeFlag = true;
                   // return RedirectToAction(nameof(Index));
                }
                else if (currentLoanCountInt >= maxLoanInt)
                {
                    ModelState.AddModelError("Error", "Maximum loan limit exceeded.");
                }
                else
                {
                    //ShowAgePugenaMessage
                    //FlashMessage.Warning("Your error message");
                    ModelState.AddModelError("Error", "You are under-age to rent this DVD copy.");
                  //  return RedirectToAction("Error", "Home");
                    
                }
                if (chargeFlag == true)
                {
                    ModelState.AddModelError("Error", "DVD loan successful. The total amount is : " + charge);
                }


            }
            ViewData["CopyNumber"] = new SelectList(_context.DVDCopies, "CopyNumber", "CopyNumber", loan.CopyNumber);
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypes, "LoanTypeNumber", "LoanTypeNumber", loan.LoanTypeNumber);
            ViewData["MemberNumber"] = new SelectList(_context.Members, "MemberNumber", "MemberFirstName", loan.MemberNumber);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["CopyNumber"] = new SelectList(_context.DVDCopies, "CopyNumber", "CopyNumber", loan.CopyNumber);
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypes, "LoanTypeNumber", "LoanTypeNumber", loan.LoanTypeNumber);
            ViewData["MemberNumber"] = new SelectList(_context.Members, "MemberNumber", "MemberFirstName", loan.MemberNumber);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanNumber,DateOut,DateDue,DateRetured,LoanTypeNumber,CopyNumber,MemberNumber")] Loan loan)
        {
            var memberList = _context.Members;
            var dvdCopyList = _context.DVDCopies;
            var dvdTitleList = _context.DVDTitles;
            var studio = _context.Studios;
            var producer = _context.Producers;
            var loans = _context.Loans;
            var dvdCategory = _context.DVDCategory;
            var selectedLoanNumber = loan.LoanNumber;
            var selectedLoanList = loans.Where(x => x.LoanNumber == selectedLoanNumber).ToList();
            var selectedLoanFirstOrDefault = loans.Where(x => x.LoanNumber == selectedLoanNumber).FirstOrDefault();
            if (id != loan.LoanNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try

                {
                    var dvdTitle = selectedLoanList.Join(dvdCopyList,
                    loan => loan.CopyNumber,
                    copy => copy.CopyNumber,
                    (loan, copy) => copy
                    ).Join(dvdTitleList,
                    copy => copy.DVDNumber,
                    title => title.DVDNumber,
                    (copy, title) => title
                 );
                    var penaltyCharge = dvdTitle.FirstOrDefault().PenaltyCharge;
                    //charge calculation
                    var loanDuration = (loan.DateDue - loan.DateOut).TotalDays;
                    var penaltyAmount = loanDuration * (Decimal.ToDouble(penaltyCharge));
                    var penaltyFlag = false;
                    if (selectedLoanFirstOrDefault.DateRetured == null) {
                        selectedLoanFirstOrDefault.DateRetured = DateTime.Now;
                        _context.Loans.Update(selectedLoanFirstOrDefault);
                        await _context.SaveChangesAsync();
                    }
                    var dateComparision = DateTime.Compare(loan.DateDue, (DateTime)selectedLoanFirstOrDefault.DateRetured);
                    if (dateComparision < 0)
                    {
                        penaltyFlag = true;
                    }
                    if (penaltyFlag == true)
                    {
                        ModelState.AddModelError("Error", "DVD returned late! The total penalty amount is : " + penaltyCharge);
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.LoanNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            ViewData["CopyNumber"] = new SelectList(_context.DVDCopies, "CopyNumber", "CopyNumber", loan.CopyNumber);
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypes, "LoanTypeNumber", "LoanTypeNumber", loan.LoanTypeNumber);
            ViewData["MemberNumber"] = new SelectList(_context.Members, "MemberNumber", "MemberFirstName", loan.MemberNumber);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.DVDCopy)
                .Include(l => l.LoanType)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(m => m.LoanNumber == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanNumber == id);
        }
    }
}
