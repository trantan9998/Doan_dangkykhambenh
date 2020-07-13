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
    public class QL_KhoaController : Controller
    {
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();

        // GET: ql_website/QL_Khoa
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult quanlykhoa()
        {
            return View(db.khoas.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khoa k = db.khoas.Find(id);
            if (k == null)
            {
                return HttpNotFound();
            }
            return View(k);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(khoa imageModel)
        {
           
            using (QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1())
            {
                db.khoas.Add(imageModel);
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
            khoa dk = db.khoas.Find(id);
            if (dk == null)
            {
                return HttpNotFound();
            }
            return View(dk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(khoa k)
        {
            if (ModelState.IsValid)
            {

                db.Entry(k).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("dsdangky_online");
            }
            return View(k);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khoa tt = db.khoas.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            khoa k = db.khoas.SingleOrDefault(n => n.id_khoa == id);
            if (ModelState.IsValid)
            {
                db.khoas.Remove(k);
                db.SaveChanges();
            }
            return RedirectToAction("quanlykhoa");
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