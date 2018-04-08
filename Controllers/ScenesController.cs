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
    public class ScenesController : Controller
    {
        private readonly MaltaMoviesContext _context;

        public ScenesController(MaltaMoviesContext context)
        {
            _context = context;
        }

        // GET: Scenes
        public async Task<IActionResult> Index()
        {
            var maltaMoviesContext = _context.Scenes
                .Include(s => s.LocationAlias)
                .Include(s => s.LocationSite)              
                .Include(s => s.Title);
                
            return View(await maltaMoviesContext.ToListAsync());

        }

        // GET: Scenes/Details/5
        public async Task<IActionResult> Details(int? id) // aka 'Details'
        {
            if (id == null)
            {
                return NotFound();
            }

            var scene = await _context.Scenes
                .Include(s => s.LocationAlias)
                .Include(s => s.LocationSite)
                .Include(s => s.LocationSite.LocationPlace)                
                .Include(s => s.Title)
                .SingleOrDefaultAsync(m => m.SceneId == id);
            if (scene == null)
            {
                return NotFound();
            }

           // return View(scene);
            return PartialView("_SceneModal",scene);
        }

        //DISABLE CRUD
        // GET: Scenes/Create
        //public IActionResult Create()
        //{
        //    ViewData["LocationAliasId"] = new SelectList(_context.LocationAliases, "LocationAliasId", "LocationAliasCountry");
        //    ViewData["LocationSiteId"] = new SelectList(_context.LocationSites, "LocationSiteId", "LocationSiteName");
        //    ViewData["TitleId"] = new SelectList(_context.Movies, "TitleId", "Title");
        //    return View();
        //}

        //// POST: Scenes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("SceneId,TitleId,LocationSiteId,LocationAliasId,Notes,SceneOrder,TitleList")] Scene scene)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(scene);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LocationAliasId"] = new SelectList(_context.LocationAliases, "LocationAliasId", "LocationAliasCountry", scene.LocationAliasId);
        //    ViewData["LocationSiteId"] = new SelectList(_context.LocationSites, "LocationSiteId", "LocationSiteName", scene.LocationSiteId);
        //    ViewData["TitleId"] = new SelectList(_context.Movies, "TitleId", "Title", scene.TitleId);
        //    return View(scene);
        //}


        //DISABLE CRUD
        // GET: Scenes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var scene = await _context.Scenes.SingleOrDefaultAsync(m => m.SceneId == id);
        //    if (scene == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["LocationAliasId"] = new SelectList(_context.LocationAliases, "LocationAliasId", "LocationAliasCountry", scene.LocationAliasId);
        //    ViewData["LocationSiteId"] = new SelectList(_context.LocationSites, "LocationSiteId", "LocationSiteName", scene.LocationSiteId);
        //    ViewData["TitleId"] = new SelectList(_context.Movies, "TitleId", "Title", scene.TitleId);
        //    return View(scene);
        //}

        //// POST: Scenes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("SceneId,TitleId,LocationSiteId,LocationAliasId,Notes,SceneOrder,TitleList")] Scene scene)
        //{
        //    if (id != scene.SceneId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(scene);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SceneExists(scene.SceneId))
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
        //    ViewData["LocationAliasId"] = new SelectList(_context.LocationAliases, "LocationAliasId", "LocationAliasCountry", scene.LocationAliasId);
        //    ViewData["LocationSiteId"] = new SelectList(_context.LocationSites, "LocationSiteId", "LocationSiteName", scene.LocationSiteId);
        //    ViewData["TitleId"] = new SelectList(_context.Movies, "TitleId", "Title", scene.TitleId);
        //    return View(scene);
        //}

        //DISABLE CRUD
        // GET: Scenes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var scene = await _context.Scenes
        //        .Include(s => s.LocationAlias)
        //        .Include(s => s.LocationSite)
        //        .Include(s => s.Title)
        //        .SingleOrDefaultAsync(m => m.SceneId == id);
        //    if (scene == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(scene);
        //}

        //// POST: Scenes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var scene = await _context.Scenes.SingleOrDefaultAsync(m => m.SceneId == id);
        //    _context.Scenes.Remove(scene);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool SceneExists(int id)
        {
            return _context.Scenes.Any(e => e.SceneId == id);
        }

        public ActionResult ViewLyubomir()
        {
            return PartialView("_SceneModal");
        }
    }
}
