using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merp.Web.Site.Models.TrialViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Merp.Web.Site.Controllers
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

        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Trial()
        {

            var film1 = new Film() { Title = "Matrix", MovieLength = 120 } ;
            var film2 = new Film() { Title = "Reloaded", MovieLength = 130 };
            var model = new List<Film>();
            model.Add(film1);
            model.Add(film2);

            return View(model);
        }

        [Authorize]
        public IActionResult Bot()
        {
            return View();
        }
    }
}
