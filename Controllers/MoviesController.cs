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
        public async Task<IActionResult> Index(string searchString)
        {
            
            // Only list movie titles that actually have a scene
            // i.e T-SQL'  'SELECT * FROM Movies WHERE TitleId IN(SELECT TitledId FROM Scenes)'
            var movies = from m in _context.Movies
                         orderby m.ParsedTitle
                         where (from s in _context.Scenes
                                select s.TitleId)
                                .Contains(m.TitleId)
                         where m.ExcludeTitle == false
                         where m.RegionId == GlobalSettings.RegionId
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
      

        //DISABLE CRUD
        // GET: Movies/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Movies/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TitleId,Title,TitleYear,ImdbUrl,Summary,ExcludeTitle,Widescreen")] Movie movie)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(movie);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(movie);
        //}


        //DISABLE CRUD
        // GET: Movies/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movies.SingleOrDefaultAsync(m => m.TitleId == id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(movie);
        //}

        //// POST: Movies/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("TitleId,Title,TitleYear,ImdbUrl,Summary,ExcludeTitle,Widescreen")] Movie movie)
        //{
        //    if (id != movie.TitleId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(movie);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MovieExists(movie.TitleId))
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
        //    return View(movie);
        //}

        //DISABLE CRUD
        // GET: Movies/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movies
        //        .SingleOrDefaultAsync(m => m.TitleId == id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movie);
        //}

        //// POST: Movies/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var movie = await _context.Movies.SingleOrDefaultAsync(m => m.TitleId == id);
        //    _context.Movies.Remove(movie);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.TitleId == id);
        }


        //TODO:
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
