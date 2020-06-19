using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using doan_qldkonline.Models;

namespace doan_qldkonline.Areas.ql_website.Controllers
{
    public class QL_hosobenhnhanController : Controller
    {
        QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities();
        // GET: ql_website/QL_hosobenhnhan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dsdangky_online()
        {
            return View(db.datlichkhams.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            datlichkham tt = db.datlichkhams.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        
        public ViewResult Edit(int id)
        {
            datlichkham dlk = db.datlichkhams.FirstOrDefault(p => p.id_benhnhan == id);
            return View(dlk);
        }
        //public ActionResult Edit(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    datlichkham dlk = db.datlichkhams.Find(id);
        //    if (dlk == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dlk);
        //}
        [ValidateAntiForgeryToken]
        public ActionResult Edit(datlichkham dlk)
        {

            if (ModelState.IsValid)
            {
                db.Entry(dlk).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("dsdangky_online");
            }
            return View(dlk);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            datlichkham tt = db.datlichkhams.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            datlichkham dlk = db.datlichkhams.SingleOrDefault(n => n.id_benhnhan == id);
            if (ModelState.IsValid)
            {
                db.datlichkhams.Remove(dlk);
                db.SaveChanges();
            }
            return RedirectToAction("dsdangky_online");
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