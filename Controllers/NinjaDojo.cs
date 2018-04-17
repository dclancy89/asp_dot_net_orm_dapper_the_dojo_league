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
                    Description = ninja.Description
                };

                if(ninja.dojo.Id == 0)
                {
                    newNinja.dojo = new Dojo
                    {
                        Id = 0,
                        DojoName = "Rogue",
                        DojoLocation = "None",
                        Description = "None"
                    };
                }
                else
                {
                    newNinja.dojo = ninja.dojo;
                }

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
