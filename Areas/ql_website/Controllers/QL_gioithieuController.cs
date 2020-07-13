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
    public class QL_gioithieuController : Controller
    {
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();

        // GET: ql_website/QL_gioithieu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult trangchu_nguoidung()
        {
            return View(db.Thietlap_Trangchu.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thietlap_Trangchu tt = db.Thietlap_Trangchu.Find(id);
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
        public ActionResult Create(Thietlap_Trangchu imageModel)
        {
            //nội dung 1
            string filename = Path.GetFileNameWithoutExtension(imageModel.ImageFile1.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile1.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.hinhanh1 = "~/hinh_gioithieu/" + filename;
            filename = Path.Combine(Server.MapPath("~/hinh_gioithieu/"), filename);
            imageModel.ImageFile1.SaveAs(filename);

            //nội dung 2
            string filename2 = Path.GetFileNameWithoutExtension(imageModel.ImageFile2.FileName);
            string extension2 = Path.GetExtension(imageModel.ImageFile2.FileName);
            filename2 = filename2 + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.hinhanh2 = "~/hinh_gioithieu/" + filename2;
            filename2 = Path.Combine(Server.MapPath("~/hinh_gioithieu/"), filename2);
            imageModel.ImageFile2.SaveAs(filename2);

            using (QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1())
            {
                db.Thietlap_Trangchu.Add(imageModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            return Redirect("trangchu_nguoidung");
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
            Thietlap_Trangchu tt = db.Thietlap_Trangchu.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Thietlap_Trangchu imageModel)
        {
            if (ModelState.IsValid)
            {
                //nội dung 1
                string filename = Path.GetFileNameWithoutExtension(imageModel.ImageFile1.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile1.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                imageModel.hinhanh1 = "~/hinh_gioithieu/" + filename;
                filename = Path.Combine(Server.MapPath("~/hinh_gioithieu/"), filename);
                imageModel.ImageFile1.SaveAs(filename);

                //nội dung 2
                string filename2 = Path.GetFileNameWithoutExtension(imageModel.ImageFile2.FileName);
                string extension2 = Path.GetExtension(imageModel.ImageFile2.FileName);
                filename2 = filename2 + DateTime.Now.ToString("yymmssfff") + extension;
                imageModel.hinhanh2 = "~/hinh_gioithieu/" + filename2;
                filename2 = Path.Combine(Server.MapPath("~/hinh_gioithieu/"), filename2);
                imageModel.ImageFile2.SaveAs(filename2);

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
            Thietlap_Trangchu tt = db.Thietlap_Trangchu.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Thietlap_Trangchu tc = db.Thietlap_Trangchu.SingleOrDefault(n => n.id == id);
            if (ModelState.IsValid)
            {
                db.Thietlap_Trangchu.Remove(tc);
                db.SaveChanges();
            }
            return RedirectToAction("trangchu_nguoidung");
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