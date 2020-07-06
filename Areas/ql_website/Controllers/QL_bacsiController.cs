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
    public class QL_bacsiController : Controller
    {
        // GET: ql_website/QL_bacsi
        QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult quanlybacsi()
        {
            return View(db.bacsis.ToList());
        }
        public ActionResult timkiem(string timkiem)
        {
            return View(db.bacsis.Where(x => x.hovaten.Contains(timkiem) || timkiem == null).ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bacsi bs = db.bacsis.Find(id);
            if (bs == null)
            {
                return HttpNotFound();
            }
            return View(bs);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(bacsi imageModel)
        {
            string filename = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.hinhanh = "~/hinh_bacsi/" + filename;
            filename = Path.Combine(Server.MapPath("~/hinh_bacsi/"), filename);
            imageModel.ImageFile.SaveAs(filename);
            using (QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities())
            {
                db.bacsis.Add(imageModel);
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
            bacsi dk = db.bacsis.Find(id);
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
                imageModel.hinhanh = "~/hinh_bacsi/" + filename;
                filename = Path.Combine(Server.MapPath("~/hinh_bacsi/"), filename);
                imageModel.ImageFile.SaveAs(filename);
                db.Entry(imageModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }

            return View(imageModel);
        }
        //[HttpGet]
        //public ActionResult Create_bacsi()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create_bacsi(bacsi imag_bacsi)
        //{
        //    string filename = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
        //    string extension = Path.GetExtension(imageModel.ImageFile.FileName);
        //    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
        //    imageModel.hinhanh = "~/hinh_tintuc/" + filename;
        //    filename = Path.Combine(Server.MapPath("~/hinh_tintuc/"), filename);
        //    imageModel.ImageFile.SaveAs(filename);
        //    using (QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities())
        //    {
        //        db.tintucs.Add(imageModel);
        //        db.SaveChanges();
        //    }
        //    ModelState.Clear();
        //    return Redirect("danhsachbaiviet");
        //}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bacsi tt = db.bacsis.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            bacsi bs = db.bacsis.SingleOrDefault(n => n.id_bacsi == id);
            if (ModelState.IsValid)
            {
                db.bacsis.Remove(bs);
                db.SaveChanges();
            }
            return RedirectToAction("quanlybacsi");
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