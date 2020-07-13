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
    public class QL_BaivietController : Controller
    {
        // GET: ql_website/QL_Baiviet
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult danhsachbaiviet()
        {
            return View(db.tintucs.ToList());
            //using (QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities())
            //{
            //    var empList = db.tintucs.ToList<tintuc>();
            //    return Json(new { data=empList }, JsonRequestBehavior.AllowGet);
            //}
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tintuc tt = db.tintucs.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(FormCollection f, tintuc imageModel)
        {
            string filename = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.hinhanh = "~/hinh_tintuc/" + filename;
            filename = Path.Combine(Server.MapPath("~/hinh_tintuc/"), filename);

            imageModel.ImageFile.SaveAs(filename);

            using (QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1())
            {
                db.tintucs.Add(imageModel);
                db.SaveChanges();   
            }
            ModelState.Clear();
            return Redirect("danhsachbaiviet");
        }
        //[httpget]
        //public actionresult view(int id)
        //{
        //    tintuc imagemodel = new tintuc();

        //    using (ql_dkkhambenh_onlineentities7 db = new ql_dkkhambenh_onlineentities7())
        //    {
        //        imagemodel = db.tintucs.where(x => x.id_tintuc == id).firstordefault();
        //    }

        //    return view(imagemodel);
        //}
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tintuc tt = db.tintucs.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tintuc imageModel)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                imageModel.hinhanh = "~/hinh_tintuc/" + filename;
                filename = Path.Combine(Server.MapPath("~/hinh_tintuc/"), filename);
                imageModel.ImageFile.SaveAs(filename);
                db.Entry(imageModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("danhsachbaiviet");
            }
            return View(imageModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tintuc tt = db.tintucs.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            tintuc tintuc = db.tintucs.SingleOrDefault(n => n.id_tintuc == id);
            if (ModelState.IsValid)
            {
                db.tintucs.Remove(tintuc);
                db.SaveChanges();
            }
            return RedirectToAction("danhsachbaiviet");
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