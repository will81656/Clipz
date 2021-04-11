using Clipz.Models.YardWorkModels;
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
    public class YardWorkController : Controller
    {
        private YardWorkService CreateWorkService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new YardWorkService(userId);
            

            return service;
        }
        public ActionResult Index()
        {
            var service = CreateWorkService();
            var model = service.GetYardWork();

            return View(model);
        }
        
        
        public ActionResult Create()
        {
            return View();
        }


        // POST: YardWork/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(YardWorkCreate model)
        {
            if (!ModelState.IsValid)
            {
                //Populate();
                return View(model);
            }

            var service = CreateWorkService();

            if (service.CreateYardWork(model))
            {
                TempData["SaveResult"] = "Your YardWork was created.";
                return RedirectToAction("Index");
            }

            //Populate();
            ModelState.AddModelError("", "YardWork could not be created");
            return View(model);
        }

        // /Get: YardWork/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateWorkService();
            var model = service.GetYardWorkById(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {

            var service = CreateWorkService();
            var detail = service.GetYardWorkById(id);
            var model =
                new YardWorkEdit
                {
                    YardWorkId = detail.YardWorkId,
                    YardWorkType = detail.YardWorkType,
                    Description = detail.Description
                };
            //Populate();
            return View(model);
        }

        // POST: YardWork/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, YardWorkEdit model)
        {
            if (!ModelState.IsValid)
            {
                //Populate();

                return View(model);
            }

            if (model.YardWorkId != id)
            {
                //Populate();
                ModelState.AddModelError("", "YardWork does not exist");
                return View(model);
            }

            var service = CreateWorkService();

            if (service.UpdateYardWork(model))
            {
                TempData["SaveResult"] = "Your YardWork was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your YardWork could not be updated.");
            return View();
        }

        // GET: YardWork/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateWorkService();
            var model = service.GetYardWorkById(id);

            return View(model);
        }

        // POST: YardWork/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteYardWork(int id)
        {
            var service = CreateWorkService();
            service.GetYardWorkById(id);

            TempData["SaveResult"] = "Your YardWork  was deleted.";
            return RedirectToAction("Index");
        }

    }
}