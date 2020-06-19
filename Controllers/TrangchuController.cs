using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using doan_qldkonline.Models;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace doan_qldkonline.Controllers
{
    public class TrangchuController : Controller
    {
        QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities();
        
        // GET: Trangchu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Trangchu()
        {
            var tongSoDatLich = db.datlichkhams.OrderByDescending(model => model.id_benhnhan);
            ViewBag.tongSoDatLich = tongSoDatLich.Count();

            var tongSoBacSi = db.bacsis.OrderByDescending(model => model.id_bacsi);
            ViewBag.tongSoBacSi = tongSoBacSi.Count();

            var tongSohoso = db.Hosobenhnhans.OrderByDescending(model => model.id_hoso);
            ViewBag.tongSohoso = tongSohoso.Count();

            var tongSobaiviet = db.tintucs.OrderByDescending(model => model.id_tintuc);
            ViewBag.tongSobaiviet = tongSobaiviet.Count();

            return View(db.tintucs.ToList());
        }
      
  
        public ActionResult dichvu()
        {
            return View();
        }
        public ActionResult bacsipartial()
        {
            var bs = db.bacsis.Take(4).ToList();
            return View(bs);
        }

        // TIN TỨC SỨC KHỎE
        public ActionResult tintuc()
        {
            return View(db.tintucs.ToList());
        }
     
     
        public ActionResult tintucpartial()
        {
            var tt = db.tintucs.Take(5).ToList();
            return View(tt);
        }
       
        public ActionResult tintucchitiet(int id_tt =0)
        {
            tintuc tt = db.tintucs.SingleOrDefault(m => m.id_tintuc == id_tt);
            if (tt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tt);
        }
        // GÓI KHÁM SỨC KHỎE
        public ActionResult goikham()
        {
            return View(db.goi_kham_suc_khoe.ToList());
        }
        public ActionResult goikham_partial()
        {
            var gk = db.goi_kham_suc_khoe.Take(10).ToList();
            return View(gk);
        }
        public ActionResult goikham_chitiet(int id_gk = 0)
        {
            goi_kham_suc_khoe gk = db.goi_kham_suc_khoe.SingleOrDefault(m => m.id_goikham == id_gk);
            if (gk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(gk);
        }
        //[HttpGet]
        //public ActionResult datlichkham()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult datlichkham(datlichkham dl)
        //{
        //    db.datlichkhams.Add(dl);
        //    db.SaveChanges();
        //    ViewBag.thongbaoemail = "Vui lòng Xác thực Email để hoàn tất hồ sơ!";
        //    return View();
        //}





    }
}