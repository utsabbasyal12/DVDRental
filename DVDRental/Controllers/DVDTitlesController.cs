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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<IActionResult> Index(string searchString)
        {
            var appDBContext = _context.DVDTitles.Include(d => d.DVDCategory).Include(d => d.Producers).Include(d => d.Studios);
            return View(await appDBContext.ToListAsync());
        }

        //Feature 2
        [Authorize]
        public async Task<IActionResult> SearchDVDCopies(string searchString)
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            var dvdCopyList = _context.DVDCopies.ToList();
            //var userDetails = "HIVE MAGICK FUCKERY";
            //var userShopID = userDetails.ShopID;
            var dvdTitle = _context.DVDTitles.ToList();
            var castMember = _context.CastMembers.ToList();
            var actor = _context.Actors.ToList();

            var dvdCopiesWithSelectedActor = (from dvdCopy in dvdCopyList
                                              join dvd in dvdTitle
                                              on dvdCopy.DVDNumber equals dvd.DVDNumber
                                              join cast in castMember
                                              on dvd.DVDNumber equals cast.DVDNumber
                                              select new DVDTitleSearchCopyVM
                                              {
                                                  //Actor = act.ActorSurname,
                                                  DVDTitle = dvd.Title
                                              }).ToList()
                                              .GroupBy(x => x.DVDTitle).Select(g => new DVDTitleSearchCopyVM
                                               {
                                                   DVDTitle = g.Key,
                                                   TitleCount = g.Count()
                                               });


            if (!String.IsNullOrEmpty(searchString))
            {
                //linq1 start
                //var shopList = _context.Shop.ToList();

                var dvdTitles = dvdCopyList.Join(dvdTitle,
                            dvdCopy => dvdCopy.DVDNumber,
                            dvdTitle => dvdTitle.DVDNumber,
                            (dvdCopy, dvdTitle) => dvdTitle
                            ).Distinct();

                dvdCopiesWithSelectedActor = (from dvdCopy in dvdCopyList
                                              join dvd in dvdTitle
                                              on dvdCopy.DVDNumber equals dvd.DVDNumber
                                              join cast in castMember
                                              on dvd.DVDNumber equals cast.DVDNumber
                                              join act in actor.Where(x => x.ActorSurname == searchString)
                                              on cast.ActorId equals act.ActorId

                                              select new DVDTitleSearchCopyVM
                                              {
                                                  //Actor = act.ActorSurname,
                                                  DVDTitle = dvd.Title
                                              }).ToList()
                                                  .GroupBy(x => x.DVDTitle).Select(g => new DVDTitleSearchCopyVM
                                                   {
                                                       DVDTitle = g.Key,
                                                       TitleCount = g.Count()
                                                   });
            }


            //    //linq1 end

            return View(dvdCopiesWithSelectedActor);
        }

        [Authorize]
        public async Task<IActionResult> ListCast() {
            var dvdCopyList = _context.DVDCopies.ToList();
            //var userDetails = "HIVE MAGICK FUCKERY";
            //var userShopID = userDetails.ShopID;
            var dvdTitle = _context.DVDTitles.ToList();
            var castMember = _context.CastMembers.ToList();
            var actor = _context.Actors.ToList();
            var studio = _context.Studios.ToList();
            var producer = _context.Producers.ToList();

            var titlesAndCast = (from dt in dvdTitle
                      join s in studio
                      on dt.StudioId equals s.StudioId
                      join c in castMember
                      on dt.DVDNumber equals c.DVDNumber
                      join a in actor
                      on c.ActorId equals a.ActorId
                      join p in producer
                      on dt.ProducerNumber equals p.ProducerNumber
                      select new DVDTitleListCastVM
                      {
                          Studio = s.StudioName,
                          Producer = p.ProducerName,
                          ActorFirstName = a.ActorFirstName,
                          ActorLastName = a.ActorSurname,
                          DVDTitle = dt.Title,
                          DateReleased = dt.DateRelease
                      }).OrderBy(x=>x.DateReleased).ThenBy(x=>x.ActorLastName);

            return View(titlesAndCast);
                }
        [Authorize]
        public async Task<IActionResult> OldCopies()
        {
            var dvdCopies = _context.DVDCopies.ToList();
            var dvdTitles = _context.DVDTitles.ToList();

            var OldCopies = (from dc in dvdCopies
                            join dt in dvdTitles.Where(dt => (DateTime.Now.Subtract(dt.DateRelease).TotalDays >= 365))
                            on dc.DVDNumber equals dt.DVDNumber
                            select new OldCopiesVM
                            {
                                CopyNumber = dc.CopyNumber,
                                DateReleased = dt.DateRelease,
                                Title = dt.Title
                            });


            return View(OldCopies);
        }


        // GET: DVDTitles/Details/5
        [Authorize]
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
        [Authorize]
        public IActionResult Create()
        {
            //var dvdDropdownData = await _context.GetNewDVDDropdownsValues();

            //ViewBag.Studios = new SelectList(dvdDropdownData.Studios, "StudioId", "StudioName");
            //ViewBag.Producers= new SelectList(dvdDropdownData.Producers, "ProducerNumber", "ProducerName");
            //ViewBag.Actors = new SelectList(dvdDropdownData.Actors, "ActorId", "FirstName");

            //return View();
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorSurname");
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
        [Authorize]
        public async Task<IActionResult> Create(DVDTitleCreateVM dVDTitleCreateVM)
        {
            int studioID;
            int producerID;
            var dvdTitles = _context.DVDTitles.ToList();
            var studios = _context.Studios.ToList();
            var producers = _context.Producers.ToList();
            var dvdTitlesExists = (dvdTitles.Where(dt => dt.Title == dVDTitleCreateVM.Title).FirstOrDefault() == null)? false:true;
            var studioExists = (studios.Where(s => s.StudioName == dVDTitleCreateVM.StudioName).FirstOrDefault() == null)? false:true;
            var producerExists = (producers.Where(p => p.ProducerName == dVDTitleCreateVM.ProducerName).FirstOrDefault() == null)? false:true;
            if (ModelState.IsValid)
            {
                if (!dvdTitlesExists)
                {
                    if (studioExists)
                    {
                        studioID = studios.Where(s => s.StudioName == dVDTitleCreateVM.StudioName).FirstOrDefault().StudioId;
                    }
                    else
                    {
                        Studio st = new Studio();
                        st.StudioName = dVDTitleCreateVM.StudioName;
                        _context.Add(st);
                    await _context.SaveChangesAsync();
                        studioID = _context.Studios.ToList().Where(s => s.StudioName == st.StudioName).FirstOrDefault().StudioId;
                    }
                    if (producerExists)
                    {
                        producerID = producers.Where(p => p.ProducerName == dVDTitleCreateVM.ProducerName).FirstOrDefault().ProducerNumber;
                    }
                    else
                    {
                        Producer pr = new Producer();
                        pr.ProducerName = dVDTitleCreateVM.ProducerName;
                        _context.Add(pr);
                    await _context.SaveChangesAsync();
                        producerID = _context.Producers.ToList().Where(p => p.ProducerName == pr.ProducerName).FirstOrDefault().ProducerNumber;
                    }
                    DVDTitle dvdt = new DVDTitle();
                    dvdt.Title = dVDTitleCreateVM.Title;
                    dvdt.DateRelease = dVDTitleCreateVM.DateReleased;
                    dvdt.StudioId = studioID;
                    dvdt.ProducerNumber = producerID;
                    dvdt.StandardCharge = dVDTitleCreateVM.StandardCharge;
                    dvdt.PenaltyCharge = dVDTitleCreateVM.PenaltyCharge;
                    dvdt.CategoryNumber = dVDTitleCreateVM.CategoryNumber;
                    _context.Add(dvdt);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Error", "Sorry! This DVD title already exists.");
                }
            }
            //ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorSurname");
            //ViewData["CategoryNumber"] = new SelectList(_context.DVDCategory, "CategoryNumber", "CategoryDescription", dVDTitle.CategoryNumber);
            //ViewData["ProducerNumber"] = new SelectList(_context.Producers, "ProducerNumber", "ProducerName", dVDTitle.ProducerNumber);
            //ViewData["StudioId"] = new SelectList(_context.Studios, "StudioId", "StudioName", dVDTitle.StudioId);

            

            return View(dVDTitleCreateVM);
        }



        // GET: DVDTitles/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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

        //adding cast members
        //GET
        [Authorize]
        public async Task<IActionResult> AddCast(int? id)
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorSurname");

            return View();
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize]
        public async Task<IActionResult> AddCast(int id, List<int> CastMembers)
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorSurname");
            var dvdID = id;
            var castList = CastMembers;

            foreach (int actorID in castList )
            {
                var castMembers = _context.CastMembers.ToList();
                var dvdTitles = _context.DVDTitles.ToList();
                var lastNo = castMembers.OrderBy(cm => cm.Id).LastOrDefault().Id;

                var actorExists = (from dt in dvdTitles.Where(dt=>dt.DVDNumber==dvdID)
                                  join cm in castMembers
                                  on dt.DVDNumber equals cm.DVDNumber
                                  select cm).Where(x=>x.ActorId==actorID).FirstOrDefault() !=null;

                if (!actorExists)
                {
                CastMember cmem = new CastMember();
                cmem.DVDNumber = dvdID;
                cmem.ActorId = actorID;
                cmem.Id = lastNo+1;
                _context.Add(cmem);
                    await _context.SaveChangesAsync();

                }

                
            }

            return View();
        }
    }
}
