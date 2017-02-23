using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Knockout_JS_CRUD.Models;
using System.Web.Script.Serialization;

namespace Knockout_JS_CRUD.Controllers
{
    public class CatsController : Controller
    {
        private catKnockoutEntities db = new catKnockoutEntities();

        // GET: Cats
        public ActionResult Index()
        {
            return View(db.Cats.ToList());
        }

        // GET: Cats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cat cat = db.Cats.Find(id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }

        // GET: Cats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public String Create(Cat cat)
        {
            if (ModelState.IsValid)
            {
                if (!CatExists(cat))
                    db.Cats.Add(cat);
                else
                    return Edit(cat);
                db.SaveChanges();
                return "Cat Created";
            }
            return "Creation Failed";
        }

        private bool CatExists(Cat cat)
        {
            Cat foundCat = db.Cats.Find(cat.id);
            if(foundCat != null && !String.IsNullOrEmpty(foundCat.name))
            {
                return true;
            }
            return false;
        }

        // GET: Cats/Edit/5
        public ActionResult Edit(int? id)
        {
            Cat cat = db.Cats.Find(id);
            if (cat == null)
            {
                return null;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.InitialData = serializer.Serialize(cat);
            return View();
        }

        // POST: Cats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public String Edit([Bind(Include = "id,name,breed,sex,age")] Cat cat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cat).State = EntityState.Modified;
                db.SaveChanges();
                return "Cat Edited";
            }
            return "Edit Failed";
        }

        // GET: Cats/Delete/5
        public ActionResult Delete(int id)
        {
            Cat cat = db.Cats.Find(id);
            if (cat == null)
            {
                return null;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.InitialData = serializer.Serialize(cat);
            return View();
        }

        [HttpPost]
        public string Delete(Cat cat)
        {
            Cat catDetail = db.Cats.Find(cat.id);
            db.Cats.Remove(catDetail);
            db.SaveChanges();
            return "Cat Deleted";
        }

        // POST: Cats/Delete/5
        //[HttpPost, ActionName("Delete")]
       // public ActionResult DeleteConfirmed(int id)
       // {
       //     Cat cat = db.Cats.Find(id);
       //     db.Cats.Remove(cat);
       //     db.SaveChanges();
       //     return RedirectToAction("Index");
       // }

        public JsonResult FetchCats()
        {
            return Json(db.Cats.ToList(), JsonRequestBehavior.AllowGet);
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
