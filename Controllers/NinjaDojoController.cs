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
        public NinjaDojoController()
        {
            //Instantiate a UserFactory object that is immutable (READONLY)
            //This establishes the initial DB connection for us.
            ninjaFactory = new NinjaFactory();
        }

        [HttpGet]
        [Route("ninjas")]
        public IActionResult Ninjas()
        {
            @ViewBag.Ninjas = ninjaFactory.AllNinjas();
            return View();
        }

        // GET: /Home/
        [HttpPost]
        [Route("/ninjas/registerninja")]
        public IActionResult RegisterNinja(Ninja ninja, int dojo_id)
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
                return RedirectToAction("Ninjas", "Home");
            }
            else
            {
                ViewBag.errors = ModelState.Values;
                return View();
            }

            
        }

        [HttpPost]
        [Route("/dojos/registerdojo")]
        public IActionResult RegisterDojo()
        {
            return RedirectToAction("Dojos", "Home");
        }

    }
}
