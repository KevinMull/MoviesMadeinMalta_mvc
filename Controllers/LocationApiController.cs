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
    [Route("api/locations")]
    public class LocationApiController : Controller
    {
        private readonly MaltaMoviesContext _context;

        public LocationApiController(MaltaMoviesContext context)
        {
            _context = context;
        }

        // GET: api/locations/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var locations = from l in _context.LocationPlaces
                            .Include("LocationSites")
                            orderby l.LocationPlaceName                          
                            where l.LocationPlaceId != 41 // Excl 'N/A'  
                            where l.RegionId == GlobalSettings.RegionId
                            select l;

            return Json(await locations.ToListAsync());
        }

        // GET: api/locations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // LAMBDA
            var locationSite = await _context.LocationPlaces
                .Include(l => l.LocationSites)
                .SingleOrDefaultAsync(l => l.LocationPlaceId == id);
            

            if (locationSite == null)
            {
                return NotFound();            }

            return Json(locationSite);
        }

        [Produces("application/json")]
        [HttpGet("search")]

        [HttpPost]
        public JsonResult Search(string Prefix)
        {

            var locations = (from l in _context.LocationSites
                                // .Include("LocationPlace")
                                //    orderby l.LocationPlace.LocationPlaceName, l.LocationSiteName
                            where l.LocationSiteId != 94 // Excl 'N/A'
                            where l.LocationSiteId != 42 // Excl 'Unknowns'
                            where l.LocationSiteName.Contains(Prefix)
                          //  where l.LocationSiteId.ToString().StartsWith(Prefix) || l.LocationSiteName.ToLower().StartsWith(Prefix.ToLower())
                             where l.LocationPlace.RegionId == GlobalSettings.RegionId
                            // Only list location sites that actually have a scene 
                            //where (from s in _context.Scenes
                            //       select s.LocationSiteId)
                            //    .Contains(l.LocationSiteId)
                            select new { l.LocationSiteName});

                            return Json(locations);
        }


        //    models context = new models();
        //    //Searching records from list using LINQ query  
        //    var cust = (from c in context.Customers
        //                  wherec.CustomerID.ToString().StartsWith(Prefix) || c.ContactName.ToLower().StartsWith(Prefix.ToLower())
        //                  select new { c.ContactName, c.CustomerID });
        //    return Json(cust, JsonRequestBehavior.AllowGet);
        //}
        //public async Task<IActionResult> Search()
        //{

        //    try
        //    {
        //        string term = HttpContext.Request.Query["term"].ToString();

        //        //LINQ QUERY WAY
        //        //var locations = from l in _context.LocationSites
        //        //            .Include("LocationPlace")
        //        //                    //    orderby l.LocationPlace.LocationPlaceName, l.LocationSiteName
        //        //                where l.LocationSiteId != 94 // Excl 'N/A'
        //        //                where l.LocationSiteId != 42 // Excl 'Unknowns'
        //        //                where l.LocationSiteName.Contains(term)
        //        //                where l.LocationPlace.RegionId == GlobalSettings.RegionId
        //        //                // Only list location sites that actually have a scene 
        //        //                //where (from s in _context.Scenes
        //        //                //       select s.LocationSiteId)
        //        //                //    .Contains(l.LocationSiteId)
        //        //                select l;
        //        //return Ok(locations.ToList);


        //        // LINQ METHOD LAMBDA WAY
        //        var locations = _context.LocationSites
                             
        //                     .Where(l => l.LocationSiteName.Contains(term) || l.LocationPlace.LocationPlaceName.Contains(term))
        //                     .Where(l => l.LocationSiteId != 94) // Excl 'N/A'
        //                     .Where(l => l.LocationSiteId != 42) // Excl 'Unknowns'
        //                     .Where(l => l.LocationPlace.RegionId == GlobalSettings.RegionId)
        //                     // Only list location sites that actually have a scene
        //                     .Where(l => _context.Scenes.Select(s => s.LocationSiteId).Contains(l.LocationSiteId))
        //                    .Select(l=>l.LocationSiteName).ToList();
        //        return Ok(locations);


        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }

        //}




    }
}
