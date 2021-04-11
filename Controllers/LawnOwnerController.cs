using Clipz.Models.LawnOwnerModels;
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
    public class LawnOwnerController : Controller
    {
        private LawnOwnerService CreateLawnService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LawnOwnerService(userId);

            return service;
        }
        public ActionResult Index()
        {
            var service = CreateLawnService();
            var model = service.GetLawnOwnerList();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: LawnOwner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LawnOwnerCreate model)
        {
            if (!ModelState.IsValid)
            {
                //Populate();
                return View(model);
            }

            var service = CreateLawnService();

            if (service.CreateLawnOwner(model))
            {
                TempData["SaveResult"] = "Your Lawn was created.";
                return RedirectToAction("Index");
            }

            //Populate();
            ModelState.AddModelError("", "Lawn could not be created");
            return View(model);
        }

        // Get: LawnOwner/Details/Id
        public ActionResult Details(int id)
        {
            var service = CreateLawnService();
            var model = service.GetLawnOwnerById(id);
            return View(model);
        }

        // Read: LawnOwner/Edit/{id}
        public ActionResult Edit(int id)
        {

            var service = CreateLawnService();
            var detail = service.GetLawnOwnerById(id);
            var model =
                new LawnOwnerEdit
                {
                    LocationId = detail.LocationId,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    Price = detail.Price,
                    YardWorkId = detail.YardWorkId
                };
            //Populate();
            return View(model);
        }

        // POST: LawnOwner/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LawnOwnerEdit model)
        {
            if (!ModelState.IsValid)
            {
                //Populate();

                return View(model);
            }

            if (model.LocationId != id)
            {
                //Populate();
                ModelState.AddModelError("", "Lawn does not exist");
                return View(model);
            }

            var service = CreateLawnService();

            if (service.UpdateLawnOwner(model))
            {
                TempData["SaveResult"] = "Your Lawn was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Lawn could not be updated.");
            return View();
        }

        // GET: LawnOwner/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateLawnService();
            var model = service.GetLawnOwnerById(id);

            return View(model);
        }
        // POST: LawnOwner/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLawn(int id)
        {
            var service = CreateLawnService();
            service.DeleteLawnOwner(id);

            TempData["SaveResult"] = "Your Lawn was deleted.";
            return RedirectToAction("Index");
        }
    }
}