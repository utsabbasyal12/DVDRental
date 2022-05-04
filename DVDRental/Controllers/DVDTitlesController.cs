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
    public class DVDTitlesController : Controller
    {
        private readonly AppDBContext _context;

        public DVDTitlesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: DVDTitles
        public async Task<IActionResult> Index(string searchString)
        {
            var dvdCopyList = _context.DVDCopies.ToList();
            //var userDetails = "HIVE MAGICK FUCKERY";
            //var userShopID = userDetails.ShopID;
            var dvdTitle = _context.DVDTitles.ToList();
            var castMember = _context.CastMembers.ToList();
            var actor = _context.Actors.ToList();

            //var dvdTitlesWithSelectedActor = _context.DVDTitles.Include(d => d.DVDCategory)
            //   .Include(d => d.Producers)
            //   .Include(d => d.Studios);

            var dvdTitlesWithSelectedActor = (from dvd in dvdTitle
                      join cast in castMember
                      on dvd.DVDNumber equals cast.DVDNumber
                      join act in actor
                      on cast.ActorId equals act.ActorId
                      join produc in _context.Producers.ToList()
                      on dvd.ProducerNumber equals produc.ProducerNumber
                      join stud in _context.Studios.ToList()
                      on dvd.StudioId equals stud.StudioId
                      join dvdCat in _context.DVDCategory.ToList()
                      on dvd.CategoryNumber equals dvdCat.CategoryNumber
                      select new DVDTitleIndexVM
                      {
                          Actor = act.ActorSurname,
                          DVDCategory = dvdCat.CategoryDescription.ToString(),
                          DVDTitle = dvd.Title,
                          Producer = produc.ProducerName,
                          Studio = stud.StudioName
                      }).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                //linq1 start
                //var shopList = _context.Shop.ToList();

                var dvdTitles = dvdCopyList.Join(dvdTitle,
                            dvdCopy => dvdCopy.DVDNumber,
                            dvdTitle => dvdTitle.DVDNumber,
                            (dvdCopy, dvdTitle) => dvdTitle
                            ).Distinct();

                var requestActorNumber = searchString;

                dvdTitlesWithSelectedActor = (from dvd in dvdTitle
                                            join cast in castMember
                                            on dvd.DVDNumber equals cast.DVDNumber
                                            join act in actor.Where(x => x.ActorSurname == requestActorNumber)
                                            on cast.ActorId equals act.ActorId
                                            join produc in _context.Producers.ToList()
                                            on dvd.ProducerNumber equals produc.ProducerNumber
                                            join stud in _context.Studios.ToList()
                                            on dvd.StudioId equals stud.StudioId
                                            join dvdCat in _context.DVDCategory.ToList()
                                            on dvd.CategoryNumber equals dvdCat.CategoryNumber
                                            select new DVDTitleIndexVM
                                            {
                                                Actor = act.ActorSurname,
                                                DVDCategory = dvdCat.CategoryDescription.ToString(),
                                                DVDTitle = dvd.Title,
                                                Producer = produc.ProducerName,
                                                Studio = stud.StudioName
                                            }).ToList();
            }


                //    //linq1 end
                //    //var dvds = dvdTitles.Include(d => d.Studios).Include(d => d.Producers).Include(d => d.DVDCategory).Include(d => d.CastMembers);

                //    //dvdTitlesWithSelectedActor = (IQueryable<DVDTitle,Actor,Producer,Studio,DVDCategory>)dvds.Where(title => title.actor.ActorId == requestActorNumber).Select(x => new {


                //    //});
                //    //DVDTitleIndexVM dVDTitleIndexVM = new DVDTitleIndexVM()
                //    //{
                //    //    DVDTitle = (DVDTitle) dvds.GetType().GetProperty("dvdTitle").GetValue(d, null),
                //    //    Actor = (Actor) dvds.GetType().GetProperty("actor").GetValue(, null)
                //    //};


                //}
                return View(dvdTitlesWithSelectedActor);
        }

        // GET: DVDTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDTitle = await _context.DVDTitles
                .Include(d => d.DVDCategory)
                .Include(d => d.Producers)
                .Include(d => d.Studios)
                .FirstOrDefaultAsync(m => m.DVDNumber == id);
            if (dVDTitle == null)
            {
                return NotFound();
            }

            return View(dVDTitle);
        }

        // GET: DVDTitles/Create
        public IActionResult Create()
        {
            //var dvdDropdownData = await _context.GetNewDVDDropdownsValues();

            //ViewBag.Studios = new SelectList(dvdDropdownData.Studios, "StudioId", "StudioName");
            //ViewBag.Producers= new SelectList(dvdDropdownData.Producers, "ProducerNumber", "ProducerName");
            //ViewBag.Actors = new SelectList(dvdDropdownData.Actors, "ActorId", "FirstName");
            
            //return View();

            ViewData["CategoryNumber"] = new SelectList(_context.DVDCategory, "CategoryNumber", "CategoryDescription");
            ViewData["ProducerNumber"] = new SelectList(_context.Producers, "ProducerNumber", "ProducerName");
            ViewData["StudioId"] = new SelectList(_context.Studios, "StudioId", "StudioName");

            return View();
        }

        // POST: DVDTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DVDNumber,Title,DateRelease,StandardCharge,PenaltyCharge,StudioId,ProducerNumber,CategoryNumber")] DVDTitle dVDTitle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dVDTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryNumber"] = new SelectList(_context.DVDCategory, "CategoryNumber", "CategoryDescription", dVDTitle.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Producers, "ProducerNumber", "ProducerName", dVDTitle.ProducerNumber);
            ViewData["StudioId"] = new SelectList(_context.Studios, "StudioId", "StudioName", dVDTitle.StudioId);
            return View(dVDTitle);
        }

        // GET: DVDTitles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDTitle = await _context.DVDTitles.FindAsync(id);
            if (dVDTitle == null)
            {
                return NotFound();
            }
            ViewData["CategoryNumber"] = new SelectList(_context.DVDCategory, "CategoryNumber", "CategoryDescription", dVDTitle.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Producers, "ProducerNumber", "ProducerName", dVDTitle.ProducerNumber);
            ViewData["StudioId"] = new SelectList(_context.Studios, "StudioId", "StudioName", dVDTitle.StudioId);
            return View(dVDTitle);
        }

        // POST: DVDTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DVDNumber,Title,DateRelease,StandardCharge,PenaltyCharge,StudioId,ProducerNumber,CategoryNumber")] DVDTitle dVDTitle)
        {
            if (id != dVDTitle.DVDNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dVDTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DVDTitleExists(dVDTitle.DVDNumber))
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
            ViewData["CategoryNumber"] = new SelectList(_context.DVDCategory, "CategoryNumber", "CategoryDescription", dVDTitle.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Producers, "ProducerNumber", "ProducerName", dVDTitle.ProducerNumber);
            ViewData["StudioId"] = new SelectList(_context.Studios, "StudioId", "StudioName", dVDTitle.StudioId);
            return View(dVDTitle);
        }

        // GET: DVDTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDTitle = await _context.DVDTitles
                .Include(d => d.DVDCategory)
                .Include(d => d.Producers)
                .Include(d => d.Studios)
                .FirstOrDefaultAsync(m => m.DVDNumber == id);
            if (dVDTitle == null)
            {
                return NotFound();
            }

            return View(dVDTitle);
        }

        // POST: DVDTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dVDTitle = await _context.DVDTitles.FindAsync(id);
            _context.DVDTitles.Remove(dVDTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DVDTitleExists(int id)
        {
            return _context.DVDTitles.Any(e => e.DVDNumber == id);
        }
    }
}
