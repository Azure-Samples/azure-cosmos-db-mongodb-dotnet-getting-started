﻿using System;
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
                dal.CreateTask(task);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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