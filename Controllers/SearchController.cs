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
    public class SearchController : Controller
    {
        private readonly MaltaMoviesContext _context;

        public SearchController(MaltaMoviesContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //public JsonResult SearchMovie(string Prefix)
        //{

         
        //    var movies = (from m in _context.Movies
        //                  where m.Title.Contains(Prefix)
        //                     select new { m.Title });
        //    return Json(movies, new Newtonsoft.Json.JsonSerializerSettings());
        //}


      
    }
}
