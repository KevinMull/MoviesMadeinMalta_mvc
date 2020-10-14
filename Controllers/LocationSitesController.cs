using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaltaMoviesMVCcore.Models;

namespace MaltaMoviesMVCcore.Controllers
{
    public class LocationSitesController : Controller
    {
        private readonly MaltaMoviesContext _context;

        public LocationSitesController(MaltaMoviesContext context)
        {
            _context = context;
        }

        // GET: LocationSites
        //With optional search string
        public async Task<IActionResult> Index(string searchString)
        {
            var locations = from l in _context.LocationSites
                            .Include("LocationPlace")                            
                            orderby l.LocationPlace.LocationPlaceName, l.LocationSiteName
                            where l.LocationSiteId  != 55 // Excl 'Behind the Scenes'
                            where l.LocationSiteId != 94 // Excl 'N/A'
                            where l.LocationPlace.LocationId == GlobalSettings.LocationId
                            select l;

            //LAMDA way
            //var locations = _context.LocationSites
            //   .Include(l => l.LocationPlace)
            //   .OrderBy(l => l.LocationPlace.LocationPlaceName)
            //   .ThenBy(l => l.LocationSiteName);
            //   select l;


            // Search wildcard by LocationSiteName or LocationPlaceName
            if (!String.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(l => l.LocationSiteName.Contains(searchString) ||  l.LocationPlace.LocationPlaceName.Contains(searchString));
            }
           
            return View(await locations.ToListAsync());
        }

        // GET: LocationSites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // LAMBDA
            var locationSite = await _context.LocationSites
                .Include(l => l.LocationPlace)
                .SingleOrDefaultAsync(l => l.LocationSiteId == id);

            ViewBag.Scenes = _context.Scenes
                .Where(s => s.LocationSiteId == id)
                .Include(s => s.LocationSite)
                .Include(s => s.LocationSite.LocationPlace)
                .Include(s => s.Movie)
                .OrderBy(s => s.Movie.Title).ToList();             

            //ViewBag.Scenes = from s in _context.Scenes
            //                 orderby s.SceneOrder
            //                 where s.LocationSiteId == id
            //                 //join m in _context.Movies on s.TitleId equals m.TitleId
            //                 select new {m.TitleId
            //                      //, m.Title
            //                     , s.SceneId                                 
            //                     , s.LocationSite.LocationPlace
            //                        };

            if (locationSite == null)
            {
                return NotFound();
            }

            return View(locationSite);
        }

        // GET: LocationSites/Create
        public IActionResult Create()
        {
            ViewData["LocationPlaceId"] = new SelectList(_context.LocationPlaces, "LocationPlaceId", "LocationPlaceName");
            return View();
        }

        // POST: LocationSites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("LocationSiteId,LocationSiteName,LocationPlaceId,Latitude,Longitude,MapInfoHtml")] LocationSite locationSite)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(locationSite);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LocationPlaceId"] = new SelectList(_context.LocationPlaces, "LocationPlaceId", "LocationPlaceName", locationSite.LocationPlaceId);
        //    return View(locationSite);
        //}

        // GET: LocationSites/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var locationSite = await _context.LocationSites.SingleOrDefaultAsync(m => m.LocationSiteId == id);
        //    if (locationSite == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["LocationPlaceId"] = new SelectList(_context.LocationPlaces, "LocationPlaceId", "LocationPlaceName", locationSite.LocationPlaceId);
        //    return View(locationSite);
        //}

        // POST: LocationSites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("LocationSiteId,LocationSiteName,LocationPlaceId,Latitude,Longitude,MapInfoHtml")] LocationSite locationSite)
        //{
        //    if (id != locationSite.LocationSiteId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(locationSite);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LocationSiteExists(locationSite.LocationSiteId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LocationPlaceId"] = new SelectList(_context.LocationPlaces, "LocationPlaceId", "LocationPlaceName", locationSite.LocationPlaceId);
        //    return View(locationSite);
        //}

        // GET: LocationSites/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var locationSite = await _context.LocationSites
        //        .Include(l => l.LocationPlace)
        //        .SingleOrDefaultAsync(m => m.LocationSiteId == id);
        //    if (locationSite == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(locationSite);
        //}

        //// POST: LocationSites/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var locationSite = await _context.LocationSites.SingleOrDefaultAsync(m => m.LocationSiteId == id);
        //    _context.LocationSites.Remove(locationSite);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LocationSiteExists(int id)
        //{
        //    return _context.LocationSites.Any(e => e.LocationSiteId == id);
        //}
    }
}
