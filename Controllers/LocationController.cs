using Clipz.Models.LocationModels;
using Clipz.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clipz.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private LocationService CreateLocationService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LocationService(userId);

            return service;
        }
        public ActionResult Index()
        {
            var service = CreateLocationService();
            var model = service.GetLocationList();

            return View(model);
        }


        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationCreate model)
        {
            if (!ModelState.IsValid)
            {
                //Populate();
                return View(model);
            }

            var service = CreateLocationService();

            if (service.CreateLocation(model))
            {
                TempData["SaveResult"] = "Your Location was created.";
                return RedirectToAction("Index");
            }

            //Populate();
            ModelState.AddModelError("", "Location could not be created");
            return View(model);
        }

        // Read: YardWork/Edit/{id}
        public ActionResult Edit(int id)
        {

            var service = CreateLocationService();
            var detail = service.GetLocationById(id);
            var model =
                new LocationEdit
                {
                    LocationId = detail.LocationId,
                    City = detail.Address,
                    LocationName = detail.LocationName,
                    State = detail.State,
                    Address = detail.Address
                };
            //Populate();
            return View(model);
        }

        // POST: Location/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LocationEdit model)
        {
            if (!ModelState.IsValid)
            {
                //Populate();

                return View(model);
            }

            if (model.LocationId != id)
            {
                //Populate();
                ModelState.AddModelError("", "Location does not exist");
                return View(model);
            }

            var service = CreateLocationService();

            if (service.UpdateLocation(model))
            {
                TempData["SaveResult"] = "Your Location was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Location could not be updated.");
            return View();
        }

        // POST: Location/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var service = CreateLocationService();
            service.DeleteLocation(id);

            TempData["SaveResult"] = "Your Location was deleted.";
            return RedirectToAction("Index");
        }
    }
}