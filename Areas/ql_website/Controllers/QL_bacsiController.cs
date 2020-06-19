using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using doan_qldkonline.Models;
using System.Web.Mvc;

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
        public ActionResult chitiet(int? id_bacsi)
        {
            if (id_bacsi == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bacsi ct = db.bacsis.Find(id_bacsi);
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(id_bacsi);
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

    }
}