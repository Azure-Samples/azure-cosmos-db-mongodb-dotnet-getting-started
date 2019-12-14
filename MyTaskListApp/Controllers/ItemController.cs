using System;
using System.Web.Mvc;
using MyTaskListApp.Models;

namespace MyTaskListApp.Controllers
{
    public class ItemController : Controller, IDisposable
    {
        private Dal dal = new Dal();
        private bool disposed = false;
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
                task.CreatedDate = DateTime.Now;
                dal.CreateTask(task);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult View(Guid Id)
        {
            MyTask task = dal.GetTask(Id);
            return View(task);
        }

        public ActionResult Edit(Guid Id)
        {
            MyTask task = dal.GetTask(Id);
            return View(task);
        }

        public ActionResult Delete(Guid Id)
        {
            dal.DeleteTask(Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "CreatedDate")] MyTask task)
        {
            if (ModelState.IsValid)
            {
                dal.UpdateTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        # region IDisposable

        new protected void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        new protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dal.Dispose();
                }
            }

            this.disposed = true;
        }

        # endregion

    }
}