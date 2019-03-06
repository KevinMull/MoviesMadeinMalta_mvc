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
                            where l.LocationPlaceId != 41 // Excl 'Behind the Scenes'
                            where l.LocationPlaceId != 94 // Excl 'N/A'                            
                            select l;
           
            //var locationplaces = _context
            //    .LocationPlaces
            //    .Include("LocationSites")
            //        .Select(lp => new LocationPlace
            //        {
            //            LocationPlaceId = lp.LocationPlaceId,
            //            LocationPlaceName = lp.LocationPlaceName,
            //            LocationSites = lp.LocationSites.Select(ls => new LocationSite
            //            {
            //                LocationSiteId = ls.LocationSiteId,
            //                LocationSiteName = ls.LocationSiteName

            //            })
            //        })
            //    });



            //orderby lp.LocationPlaceName
            //where lp.LocationPlaceId != 41 // Excl 'Behind the Scenes'
            //where lp.LocationPlaceId != 94 // Excl 'N/A'                            
            //select lp;

            //locations = locations.OrderBy(l => l.LocationPlaceName);


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

            //ViewBag.Scenes = _context.Scenes
            //    .Where(s => s.LocationSiteId == id)
            //    .Include(s => s.LocationSite)
            //    .Include(s => s.LocationSite.LocationPlace)
            //    .Include(s => s.Movie)
            //    .OrderBy(s => s.Movie.Title).ToList();
            

            if (locationSite == null)
            {
                return NotFound();
            }

            return Json(locationSite);
        }

       

       
    }
}
