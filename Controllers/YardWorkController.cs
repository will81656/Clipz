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
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new YardWorkService(userId);
            var model = service.GetYardWorkList();

            return View(model);
        }
    }
}