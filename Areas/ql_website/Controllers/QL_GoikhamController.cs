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

    public class QL_GoikhamController : Controller
    {
        QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities();
        // GET: ql_website/QL_Goikham
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult quanlygoikham()
        {
            return View(db.goi_kham_suc_khoe.ToList());
        }
        public ActionResult timkiem(string timkiem)
        {
            return View(db.goi_kham_suc_khoe.Where(x => x.gioithieu.Contains(timkiem) || timkiem == null).ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goi_kham_suc_khoe gk = db.goi_kham_suc_khoe.Find(id);
            if (gk == null)
            {
                return HttpNotFound();
            }
            return View(gk);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(goi_kham_suc_khoe imageModel)
        {
            string filename = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.hinhanh = "~/hinh_goikham/" + filename;
            filename = Path.Combine(Server.MapPath("~/hinh_goikham/"), filename);
            imageModel.ImageFile.SaveAs(filename);
            using (QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities())
            {
                db.goi_kham_suc_khoe.Add(imageModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            //ViewBag.message = "Bạn đã thêm thành công";
            return Redirect("quanlygoikham");
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goi_kham_suc_khoe dk = db.goi_kham_suc_khoe.Find(id);
            if (dk == null)
            {
                return HttpNotFound();
            }
            return View(dk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(bacsi imageModel)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                imageModel.hinhanh = "~/hinh_goikham/" + filename;
                filename = Path.Combine(Server.MapPath("~/hinh_goikham/"), filename);
                imageModel.ImageFile.SaveAs(filename);
                db.Entry(imageModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return View(imageModel);
        }
       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goi_kham_suc_khoe tt = db.goi_kham_suc_khoe.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            goi_kham_suc_khoe gk = db.goi_kham_suc_khoe.SingleOrDefault(n => n.id_goikham == id);
            if (ModelState.IsValid)
            {
                db.goi_kham_suc_khoe.Remove(gk);
                db.SaveChanges();
            }
            return RedirectToAction("quanlygoikham");
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
    
   