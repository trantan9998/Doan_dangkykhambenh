using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using doan_qldkonline.Models;


namespace doan_qldkonline.Areas.ql_website.Controllers
{
    public class HosobenhnhanController : Controller
    {
        // GET: ql_website/Hosobenhnhan
        public ActionResult Index()
        {
            return View();
        }
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();
        // GET: ql_website/QL_hosobenhnhan
      
        public ActionResult quanlyhs()
        {
            return View(db.Hosobenhnhans.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hosobenhnhan dkkb = db.Hosobenhnhans.Find(id);
            if (dkkb == null)
            {
                return HttpNotFound();
            }
            return View(dkkb);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hosobenhnhan imageModel)
        {
            using (QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1())
            {
                db.Hosobenhnhans.Add(imageModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            //ViewBag.message = "Bạn đã thêm thành công";
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hosobenhnhan dk = db.Hosobenhnhans.Find(id);
            if (dk == null)
            {
                return HttpNotFound();
            }
            return View(dk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hosobenhnhan hsbn)
        {
            if (ModelState.IsValid)
            {

                db.Entry(hsbn).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("quanlyhs");
            }
            return View(hsbn);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hosobenhnhan tt = db.Hosobenhnhans.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Hosobenhnhan dlk = db.Hosobenhnhans.SingleOrDefault(n => n.id_hoso == id);
            if (ModelState.IsValid)
            {
                db.Hosobenhnhans.Remove(dlk);
                db.SaveChanges();
            }
            return RedirectToAction("quanlyhs");
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