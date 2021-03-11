using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaltaMoviesMVCcore.Models;
using Microsoft.Extensions.Options;

namespace MaltaMoviesMVCcore.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MaltaMoviesContext _context;

        public MoviesController(MaltaMoviesContext context)
        {
            _context = context;
        }


        // GET: Movies
        //With optional search string
        [HttpGet("[controller]/{regionName}")]
        public async Task<IActionResult> Index(string searchString, string regionName)
        {
            
            // Only list movie titles that actually have a scene
            // i.e T-SQL'  'SELECT * FROM Movies WHERE TitleId IN(SELECT TitledId FROM Scenes)'
            var movies = from m in _context.Movies
                         orderby m.ParsedTitle
                         where (from s in _context.Scenes
                                select s.TitleId)
                                .Contains(m.TitleId)
                         where m.ExcludeTitle == false
                        // where m.RegionId == GlobalSettings.RegionId
                         where m.RegionName == regionName
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m=>m.Title.Contains(searchString));
            }
            
            //return View(await _context.Movies.ToListAsync());
            return View(await movies.ToListAsync());
            
        }
        
              
        //[Route("[controller]/{id}")]
        [HttpGet("[controller]/{id}/{title}", Name = "GetMovie")]
        public IActionResult Details(int id, string title)
        {

            Movie movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

                        
            ViewBag.Scenes = _context.Scenes
                .Where(s => s.TitleId == id)
                .Include(s => s.LocationSite)
                .Include(s => s.LocationSite.LocationPlace)
                .OrderBy(s => s.SceneOrder).ToList();

            
            // Get the actual friendly version of the title.
            string friendlyTitle = FriendlyUrlHelper.GetFriendlyTitle(movie.Title);

            // Compare the title with the friendly title.
            if (!string.Equals(friendlyTitle, title, StringComparison.Ordinal))
            {
                // If the title is null, empty or does not match the friendly title, return a 301 Permanent
                // Redirect to the correct friendly URL.
                return this.RedirectToRoutePermanent("GetMovie", new { id = id, title = friendlyTitle });
            }

            return View(movie);
        }
      

        

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.TitleId == id);
        }


       
        [HttpPost]
        public  JsonResult AutoComplete(string prefix)
        {
            //Auto complete search
            var  movies = (from m in _context.Movies
                        // where m.ExcludeTitle == false
                         where m.Title.StartsWith(prefix)
                         //select m;
                         select new
                         {
                             label = m.Title,
                             val= m.TitleId
                             
                         }).ToList();

            return Json(_context.Movies.ToList());
    
        }
        [HttpPost]
        public ActionResult Search(string Title, string TitleId)
        {
            ViewBag.Message = "Title: " + Title + " TitleId: " + TitleId;
            return View();
        }
    }
}
