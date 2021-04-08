using Clipz.Models.MowerModels;
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
    public class MowerController : Controller
    {
        private MowerService CreateMowerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MowerService(userId);


            return service;
        }
        public ActionResult Index()
        {
            var service = CreateMowerService();
            var model = service.GetMowerList();

            return View(model);
        }


        // POST: Mower/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MowerCreate model)
        {
            if (!ModelState.IsValid)
            {
                //Populate();
                return View(model);
            }

            var service = CreateMowerService();

            if (service.CreateMower(model))
            {
                TempData["SaveResult"] = "Your Mower Profile was created.";
                return RedirectToAction("Index");
            }

            //Populate();
            ModelState.AddModelError("", "Mower Profile could not be created");
            return View(model);
        }

        // Read: Mower/Edit/{id}
        public ActionResult Edit(int id)
        {

            var service = CreateMowerService();
            var detail = service.GetMowerById(id);
            var model =
                new MowerEdit
                {
                    MowerId = detail.MowerId,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    LocationId = detail.LocationId,
                };
            //Populate();
            return View(model);
        }

        // POST: Mower/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MowerEdit model)
        {
            if (!ModelState.IsValid)
            {
                //Populate();

                return View(model);
            }

            if (model.MowerId != id)
            {
                //Populate();
                ModelState.AddModelError("", "Mower Profile does not exist");
                return View(model);
            }

            var service = CreateMowerService();

            if (service.UpdateMower(model))
            {
                TempData["SaveResult"] = "Your Mower Profile was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Mower Profile could not be updated.");
            return View();
        }

        // POST: Mower/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var service = CreateMowerService();
            service.DeleteMower(id);

            TempData["SaveResult"] = "Your Mower Profile  was deleted.";
            return RedirectToAction("Index");
        }
    }
}