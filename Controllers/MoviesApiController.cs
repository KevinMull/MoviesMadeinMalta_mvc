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
    [Route("api/movies")]
    public class MoviesApiController : Controller
    {
        private readonly MaltaMoviesContext _context;

        public MoviesApiController(MaltaMoviesContext context)
        {
            _context = context;
        }

        // GET: /api/movies/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movies = from m in _context.Movies
                                    orderby m.ParsedTitle
                                    where m.ExcludeTitle==false
                                    where m.RegionId == GlobalSettings.RegionId
                                     //inc only titles with secnes
                                    where (from s in _context.Scenes
                                    select s.TitleId)
                                    .Contains(m.TitleId)
                                    select new {
                                                 m.TitleId
                                                 ,title = m.ParsedTitle 
                                                 ,year = m.TitleYear
                                                 ,m.Summary
                                                 ,m.ImdbUrl
                                                 }
                                     ;
       
            return Json(await movies.ToListAsync());
        }


        // GET: /api/movies/5  
        [HttpGet("{id}")]
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = from m in _context.Movies    
                         where m.TitleId==id                         
                         select new
                         {
                             m.TitleId
                             ,title = m.ParsedTitle
                             ,year = m.TitleYear
                             ,m.Summary
                             ,m.ImdbUrl
                         }
                         ;

            if (movie == null)
            {
                return NotFound();
            }

            return Json(movie);
          
        }

        //Search using ALAX 
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {

            try
            {
                string term = HttpContext.Request.Query["term"].ToString();

                var movies = _context.Movies
                              .Where(m => m.Title.Contains(term))
                              .Where (m => m.ExcludeTitle == false)
                              .Where (m => m.RegionId == GlobalSettings.RegionId)
                              //TODO need to add a global variable for RegionId isntead of a const
                              .Select(m => m.Title).ToList();

                return Ok(movies);
                // await
            }
            catch
            {
                return BadRequest();
            }



        }



    }
}
