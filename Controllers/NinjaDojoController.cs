using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheDojoLeague.Models;
using TheDojoLeague.Factory;

namespace TheDojoLeague.Controllers
{
    public class NinjaDojoController : Controller
    {

        private readonly NinjaFactory ninjaFactory;
        private readonly DojoFactory dojoFactory;
        public NinjaDojoController()
        {
            //Instantiate a UserFactory object that is immutable (READONLY)
            //This establishes the initial DB connection for us.
            ninjaFactory = new NinjaFactory();
            dojoFactory = new DojoFactory();
        }

        [HttpGet]
        [Route("ninjas")]
        public IActionResult Ninjas()
        {
            @ViewBag.Ninjas = ninjaFactory.AllNinjas();
            @ViewBag.Dojos = dojoFactory.AllDojos();
            return View();
        }

        // GET: /Home/
        [HttpPost]
        [Route("/ninjas/registerninja")]
        public IActionResult RegisterNinja(Ninja ninja)
        {
            if(ModelState.IsValid)
            {
                Ninja newNinja = new Ninja
                {
                    NinjaName = ninja.NinjaName,
                    NinjaLevel = ninja.NinjaLevel,
                    DojoId = ninja.DojoId,
                    Description = ninja.Description
                    
                };

                ninjaFactory.Add(newNinja);
                return RedirectToAction("Ninjas");
            }
            else
            {
                ViewBag.errors = ModelState.Values;
                return View();
            }

            
        }

        [HttpGet]
        [Route("ninjas/{id}")]
        public IActionResult ShowNinja(int id)
        {
            ViewBag.Ninja = ninjaFactory.GetNinja(id);
            return View();
        }

        [HttpGet]
        [Route("dojos")]
        public IActionResult Dojos()
        {
            ViewBag.Dojos = dojoFactory.AllDojos();
            return View();
        }

        [HttpPost]
        [Route("/dojos/registerdojo")]
        public IActionResult RegisterDojo(Dojo dojo)
        {

            if(ModelState.IsValid)
            {
                Dojo newDojo = new Dojo
                {
                    DojoName = dojo.DojoName,
                    DojoLocation = dojo.DojoLocation,
                    Description = dojo.Description
                };

                dojoFactory.Add(newDojo);
                return RedirectToAction("Dojos");
            }
            return RedirectToAction("Dojos");
        }

    }
}
