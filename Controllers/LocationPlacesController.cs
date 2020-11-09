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
    public class LocationPlacesController : Controller
    {
        private readonly MaltaMoviesContext _context;

        public LocationPlacesController(MaltaMoviesContext context)
        {
            _context = context;
        }

       
        [HttpGet("[controller]/{regionName}")]
        public async Task<IActionResult> Index(string searchString, string regionName)

           {
            var locations = from lp in _context.LocationPlaces
                            .Include(lp => lp.LocationSites)
                            .Include(s => s.LocationSites)
                            orderby lp.LocationPlaceName
                            where lp.LocationPlaceId != 26 // Excl 'Behind the Scenes'
                            where lp.LocationPlaceId != 41 // Excl 'N/A'
                            where lp.LocationPlaceId != 22 // Excl 'Unknowns' 1
                            where lp.LocationPlaceId != 67 // Excl 'Unknowns' 2
                            where lp.RegionName == regionName
                            // Only list location sites that actually have a scene 
                            //where (from s in _context.Scenes
                            //       select s.LocationSiteId)
                            //    .Contains(lp.LocationSites.LocationSiteId)
                            select lp;
           

            // Search wildcard by LocationSiteName or LocationPlaceName
            if (!String.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(lp =>  lp.LocationPlaceName.Contains(searchString));
            }

            return View(await locations.ToListAsync());
        }

        // BY Id and PlceName 
        [Route("[controller]/{id}/{locationplacename}", Name = "GetLocationPlaceName")]
        public async  Task<IActionResult> Details(int id, string locationplacename)
        {

            var locationPlace = await _context.LocationPlaces
               //.Include(lp => lp.LocationPlace)
               .SingleOrDefaultAsync(lp => lp.LocationPlaceId == id);

            

            if (locationPlace == null)
            {
                return NotFound();
            }

            ViewBag.Scenes = _context.Scenes
                .Include(lp => lp.LocationSite)
                .Where(lp => lp.LocationSite.LocationPlaceId == id)
                .Include(s => s.LocationSite)
                .Include(s => s.Movie)
                .OrderBy(s => s.Movie.Title).ToList();

            // Get the actual friendly version of the title.
            string friendlyTitle = FriendlyUrlHelper.GetFriendlyTitle(locationplacename);

            // Compare the title with the friendly title.
            if (!string.Equals(friendlyTitle, locationplacename, StringComparison.Ordinal))
            {
                // If the title is null, empty or does not match the friendly title, return a 301 Permanent
                // Redirect to the correct friendly URL.
                return this.RedirectToRoutePermanent("GetLocationPlaceName", new { id = id, locationplacename = friendlyTitle });
            }




            return View(locationPlace);
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
