using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TheDojoLeague.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("ninjas")]
        public IActionResult Ninjas()
        {
            return View();
        }

        [HttpGet]
        [Route("dojos")]
        public IActionResult Dojos()
        {
            return View();
        }
    }
}
