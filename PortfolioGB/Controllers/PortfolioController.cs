﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortfolioGB.Models;
using System.IO;

namespace PortfolioGB.Controllers
{
    public class PortfolioController : Controller
    {
        private PortfolioDBContext db = new PortfolioDBContext();

        // GET: /Portfolio/
        public ActionResult Index()
        {
            return View(db.Portfolios.ToList());
        }

        // GET: /Portfolio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // GET: /Portfolio/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Portfolio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Title,About,Link")] Portfolio portfolio, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                var path = String.Empty;
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var randomName = Path.GetRandomFileName();

                    string[] words = fileName.Split('.');

                    var newName = words[0] + randomName + "." + words[1];

                    var filePath = Path.Combine(Server.MapPath("~/images"), newName);
                    
                    try
                    {
                        imageFile.SaveAs(filePath);
                        path = String.Format("/images/{0}", newName);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Error " + ex.Message;
                    }

                }
                portfolio.Image = path;
                db.Portfolios.Add(portfolio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(portfolio);
        }

        // GET: /Portfolio/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // POST: /Portfolio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Title,About,Link,Image")] Portfolio portfolio, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                var path = String.Empty;
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/images"), fileName);

                    try
                    {
                        imageFile.SaveAs(filePath);
                        path = String.Format("/images/{0}", fileName);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Error " + ex.Message;
                    }

                }
                else
                {
                    path = portfolio.Image;
                }

                portfolio.Image = path;
                db.Entry(portfolio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portfolio);
        }

        // GET: /Portfolio/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            
            return View(portfolio);
        }

        // POST: /Portfolio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Portfolio portfolio = db.Portfolios.Find(id);
            db.Portfolios.Remove(portfolio);
            var fileName = Path.GetFileName(portfolio.Image);
            var filePath = Path.Combine(Server.MapPath("~/images"), fileName);
            if (fileName != "")
	        {
                System.IO.File.Delete(filePath);
	        }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
