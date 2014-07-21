// TasksController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Mvc;

namespace WebApi2BookSPA.Controllers
{
    public class TasksController : Controller
    {
        public ActionResult Index()
        {
            return View("TasksView");
        }
    }
}