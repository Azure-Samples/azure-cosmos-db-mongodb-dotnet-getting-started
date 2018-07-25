using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTaskListApp.Models;
using System.Configuration;

namespace MyTaskListApp.Controllers
{
    public class HomeController : Controller
    {
        private Dal dal = new Dal();

        //
        // GET: /MyTask/
        public ActionResult Index()
        {
            return View(dal.GetAllTasks());
        }

        //
        // GET: /MyTask/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MyTask/Create

        [HttpPost]
        public ActionResult Create(MyTask task)
        {
            try
            {
                dal.CreateTask(task);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}