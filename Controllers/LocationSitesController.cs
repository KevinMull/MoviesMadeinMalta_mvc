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
                            where l.LocationSiteId != 42 // Excl 'Unknowns'
                            where l.LocationPlace.RegionId == GlobalSettings.RegionId
                            // Only list location sites that actually have a scene 
                            where (from s in _context.Scenes
                                   select s.LocationSiteId)
                                .Contains(l.LocationSiteId)
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

        // BY Id and SiteName 
        [Route("[controller]/{id}/{locationsitename}", Name = "GetLocationSiteName")]
        public async Task<IActionResult> Details(int id, string locationsitename)
        {

            var locationSite = await _context.LocationSites
               .Include(l => l.LocationPlace)
               .SingleOrDefaultAsync(l => l.LocationSiteId == id);

            if (locationSite == null)
            {
                return NotFound();
            }
           
            ViewBag.Scenes = _context.Scenes
                .Where(s => s.LocationSiteId == id)
                .Include(s => s.LocationSite)
                .Include(s => s.LocationSite.LocationPlace)
                .Include(s => s.Movie)
                .OrderBy(s => s.Movie.Title).ToList();

            // Get the actual friendly version of the title.
            string friendlyTitle = FriendlyUrlHelper.GetFriendlyTitle(locationSite.LocationSiteName);

            // Compare the title with the friendly title.
            if (!string.Equals(friendlyTitle, locationsitename, StringComparison.Ordinal))
            {
                // If the title is null, empty or does not match the friendly title, return a 301 Permanent
                // Redirect to the correct friendly URL.
                return this.RedirectToRoutePermanent("GetLocationSiteName", new { id = id, locationsitename = friendlyTitle });
            }




            return View(locationSite);
        }

        // BY ID ...GET: LocationSites/5  ** ORIG by Id only ***
        //[Route("[controller]/{id}")]
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var locationSite = await _context.LocationSites
        //        .Include(l => l.LocationPlace)
        //        .SingleOrDefaultAsync(l => l.LocationSiteId == id);

        //    ViewBag.Scenes = _context.Scenes
        //        .Where(s => s.LocationSiteId == id)
        //        .Include(s => s.LocationSite)
        //        .Include(s => s.LocationSite.LocationPlace)
        //        .Include(s => s.Movie)
        //        .OrderBy(s => s.Movie.Title).ToList();


        //    if (locationSite == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(locationSite);
        //}

        // Get only 'Unknown locations'   
        // [Route("[controller]/{id}")]
        public async Task<IActionResult> Unknown(int? id)
        {
            // int unknownId = 22; //Malta
            // int unknownId = 67; //Manchester

            var locationSite = await _context.LocationSites
                .Include(l => l.LocationPlace)
                .SingleOrDefaultAsync(l => l.LocationSiteId == id);

            ViewBag.Scenes = _context.Scenes
                .Where(s => s.LocationSiteId == id)
                .Include(s => s.LocationSite)
                .Include(s => s.LocationSite.LocationPlace)
                .Include(s => s.Movie)
                .OrderBy(s => s.Movie.Title).ToList();

         

            if (locationSite == null)
            {
                return NotFound();
            }

            return View(locationSite);
        }

        // BY Name(string) ...GET: LocationSites/Details/VallettaFortStElmo
        //public async Task<IActionResult> Details(string locationSiteName)
        //{
        //    if (locationSiteName == null)
        //    {
        //        return NotFound();
        //    }
        //    var locationSite = await _context.LocationSites
        //        .Include(l => l.LocationPlace)
        //        .SingleOrDefaultAsync(l => l.LocationSiteName == locationSiteName);

        //    ViewBag.Scenes = _context.Scenes
        //        .Where(s => s.LocationSiteId == locationSite.LocationSiteId)
        //        .Include(s => s.LocationSite)
        //        .Include(s => s.LocationSite.LocationPlace)
        //        .Include(s => s.Movie)
        //        .OrderBy(s => s.Movie.Title).ToList();

        //    if (locationSite == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(locationSite);
        //}

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
