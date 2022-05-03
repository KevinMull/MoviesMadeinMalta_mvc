using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MaltaMoviesMVCcore.Models;

namespace MaltaMoviesMVCcore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        //public IActionResult Moviemap()
        //{
            

        //    return View();
        //}
        public IActionResult Maltamoviemap() // not used
        {


            return View();
        }
        public IActionResult Manchestermoviemap()
        {


            return View();
        }

        public IActionResult Aliasmap() // notused
        {

            return View();
        }
        public IActionResult Maltaaliasmap()
        {

            return View();
        }

        public IActionResult Manchesteraliasmap()
        {

            return View();
        }





        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
