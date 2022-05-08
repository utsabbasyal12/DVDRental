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
    public class MembersController : Controller
    {
        private readonly AppDBContext _context;

        public MembersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Members
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Members.Include(m => m.MembershipCategory);
            return View(await appDBContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> SearchMemberLoan(string searchString)
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            var dvdCopyList = _context.DVDCopies.ToList();
            //var userDetails = "HIVE MAGICK FUCKERY";
            //var userShopID = userDetails.ShopID;
            var dvdTitle = _context.DVDTitles.ToList();
            var castMember = _context.CastMembers.ToList();
            var actor = _context.Actors.ToList();
            var members = _context.Members.ToList();
            var loan = _context.Loans.ToList();

            var memberLoans = from m in members
                              join l in loan
                              on m.MemberNumber equals l.MemberNumber
                              join dc in dvdCopyList
                              on l.CopyNumber equals dc.CopyNumber
                              join dt in dvdTitle
                              on dc.DVDNumber equals dt.DVDNumber
                              join produc in _context.Producers.ToList()
                              on dt.ProducerNumber equals produc.ProducerNumber
                              join stud in _context.Studios.ToList()
                              on dt.StudioId equals stud.StudioId
                              join dvdCat in _context.DVDCategory.ToList()
                              on dt.CategoryNumber equals dvdCat.CategoryNumber

                              select new SearchMemberLoanVM
                              {
                                  DVDTitle = dt.Title,
                                  CopyNumber = dc.CopyNumber,
                                  MemberFirstName = m.MemberFirstName,
                                  MemberLastName = m.MemberLastName,
                              };

            if (!String.IsNullOrEmpty(searchString))
            {
                var memberLastName = searchString;
                var currentDate = DateTime.Now;

               memberLoans = from m in members.Where(m => m.MemberLastName.ToLower() == memberLastName.ToLower())
                              join l in loan
                              .Where(l => currentDate.Subtract(l.DateOut).TotalDays <= 31)
                              on m.MemberNumber equals l.MemberNumber
                              join dc in dvdCopyList
                              on l.CopyNumber equals dc.CopyNumber
                              join dt in dvdTitle
                              on dc.DVDNumber equals dt.DVDNumber
                              join produc in _context.Producers.ToList()
                              on dt.ProducerNumber equals produc.ProducerNumber
                              join stud in _context.Studios.ToList()
                              on dt.StudioId equals stud.StudioId
                              join dvdCat in _context.DVDCategory.ToList()
                              on dt.CategoryNumber equals dvdCat.CategoryNumber

                              select new SearchMemberLoanVM
                              {
                                  DVDTitle = dt.Title,
                                  CopyNumber = dc.CopyNumber,
                                  MemberFirstName = m.MemberFirstName,
                                  MemberLastName = m.MemberLastName,
                              };


                //members.Join(loan,
                //        member => member.MemberNumber,
                //        loan => loan.MemberNumber,
                //        (member, loan) => loan
                //        ).Where(x => x.dateReturned != null)
                //        .Where(DateTime.Now() - x.dateReturned <= 31)
                //        .Where(x => x.memberNumber == selectedMember);

            }


            //    //linq1 end

            return View(memberLoans);
        }

        [Authorize]
        public async Task<IActionResult> memberLoanStatus(string searchString)
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            var dvdCopyList = _context.DVDCopies.ToList();
            //var userDetails = "HIVE MAGICK FUCKERY";
            //var userShopID = userDetails.ShopID;
            var dvdTitle = _context.DVDTitles.ToList();
            var castMember = _context.CastMembers.ToList();
            var actor = _context.Actors.ToList();
            var members = _context.Members.ToList();
            var loan = _context.Loans.ToList();



            var memberLoanStatus = (from m in members
                              join l in loan
                              on m.MemberNumber equals l.MemberNumber
                              join mCat in _context.MembershipCategories.ToList()
                              on m.MembershipCategoryNumber equals mCat.MembershipCategoryNumber
                                select new MemberLoanStatusVM
                                {
                                  MemberNumber = m.MemberNumber,
                                  MemberFirstName = m.MemberFirstName,
                                  MemberLastName = m.MemberLastName,
                                  MemberAddress = m.MemberAddress,
                                  DateReturned = l.DateRetured,
                                  MemberDOB = m.MemberDOB,
                                  MembershipCategory = mCat.MembershipCategoryDescription.ToString(),
                                  CategoryTotalLoans = mCat.MembershipCategoryTotalLoans

                              }).GroupBy(x => x.MemberNumber).Select(g => new MemberLoanStatusVM
                              {
                                  MemberNumber = g.Key,
                                  MemberFirstName = g.FirstOrDefault().MemberFirstName,
                                  MemberLastName = g.FirstOrDefault().MemberLastName,
                                  MemberAddress = g.FirstOrDefault().MemberAddress,
                                  MemberDOB = g.FirstOrDefault().MemberDOB,
                                  MembershipCategory = g.FirstOrDefault().MembershipCategory,
                                  CategoryTotalLoans = g.FirstOrDefault().CategoryTotalLoans,
                                  MemberLoanCount = g.Count(l => l.DateReturned == null),
                                  Remark = (g.FirstOrDefault().CategoryTotalLoans < g.Count(l => l.DateReturned == null)) ? "Too Many DVDs" : "Valid No. of DVDs"
                              });
            return View(memberLoanStatus);
        }

        [Authorize]
        public async Task<IActionResult> UnloanedMembers()
        {
            var members = _context.Members.ToList();
            var loans = _context.Loans.ToList();
            var dvdTitles = _context.DVDTitles.ToList();
            var dVDCopies = _context.DVDCopies.ToList();
            var currentDate = DateTime.Now;

            var loanedMembers = (from m in members
                                join l in loans.Where(l => currentDate.Subtract(l.DateOut).TotalDays <= 31)
                                on m.MemberNumber equals l.MemberNumber
                                select m).Distinct().ToList();

            var unloanedMembers = members.Except(loanedMembers).ToList();                     
                                  
            var memberLoanDetails = (from m in unloanedMembers
                                    join l in loans
                                    on m.MemberNumber equals l.MemberNumber
                                  join dc in dVDCopies
                                  on l.CopyNumber equals dc.CopyNumber
                                  join dt in dvdTitles
                                  on dc.DVDNumber equals dt.DVDNumber
                                  select new UnloanedMembersVM
                                  {
                                      DateOut = l.DateOut,
                                      DVDTitle = dt.Title,
                                      LoanedDays = Convert.ToInt32(currentDate.Subtract(l.DateOut).TotalDays),
                                      Address = m.MemberAddress,
                                      MemberFirstName = m.MemberFirstName,
                                      MemberLastName = m.MemberLastName,
                                  }).ToList();

            return View(memberLoanDetails);
        }

        // GET: Members/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.MembershipCategory)
                .FirstOrDefaultAsync(m => m.MemberNumber == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["MembershipCategoryNumber"] = new SelectList(_context.MembershipCategories, "MembershipCategoryNumber", "MembershipCategoryNumber");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("MemberNumber,MemberLastName,MemberFirstName,MemberAddress,MemberDOB,MembershipCategoryNumber")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MembershipCategoryNumber"] = new SelectList(_context.MembershipCategories, "MembershipCategoryNumber", "MembershipCategoryNumber", member.MembershipCategoryNumber);
            return View(member);
        }

        // GET: Members/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            ViewData["MembershipCategoryNumber"] = new SelectList(_context.MembershipCategories, "MembershipCategoryNumber", "MembershipCategoryNumber", member.MembershipCategoryNumber);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("MemberNumber,MemberLastName,MemberFirstName,MemberAddress,MemberDOB,MembershipCategoryNumber")] Member member)
        {
            if (id != member.MemberNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberNumber))
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
            ViewData["MembershipCategoryNumber"] = new SelectList(_context.MembershipCategories, "MembershipCategoryNumber", "MembershipCategoryNumber", member.MembershipCategoryNumber);
            return View(member);
        }

        // GET: Members/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.MembershipCategory)
                .FirstOrDefaultAsync(m => m.MemberNumber == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberNumber == id);
        }
    }
}
