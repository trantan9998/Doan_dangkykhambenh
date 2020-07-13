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
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();
        
        // GET: Trangchu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult lienhe()
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

            var bnkhambenh = db.dangkykhambenhs.OrderByDescending(model => model.id_benhnhan);
            ViewBag.bnkhambenh = bnkhambenh.Count();

            return View(db.tintucs.ToList());
        }
      
  
        public ActionResult gioithieu()
        {
            return View(db.Thietlap_Trangchu.ToList());
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
            var tt = db.tintucs.Take(4).ToList();
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
        [HttpGet]
        public ActionResult dangkykhambenh()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult dangkykhambenh([Bind(Exclude = "id_benhnhan")]dangkykhambenh dkkhambenh)
        {
            if (ModelState.IsValid)
            {
                using (QL_DKKHAMBENH_ONLINEEntities1 dbs = new QL_DKKHAMBENH_ONLINEEntities1())
                {
                    dbs.dangkykhambenhs.Add(dkkhambenh);
                    dbs.SaveChanges();
                    ViewBag.message = "BẠN ĐÃ ĐĂNG KÝ THÀNH CÔNG, vui lòng chờ bộ phận tư vấn liên hệ";
                }
                //return RedirectToAction("dangkykhambenh");

            }
            else
            {
                ViewBag.message2 = "VUI LÒNG KIỂM TRA LẠI THÔNG TIN";
            }
            return View();

            //try
            //{
            //    db.dangkykhambenhs.Add(dkkhambenh); db.SaveChanges();
            //    return RedirectToAction("dangkykhambenh");

            //}
            //catch
            //{
            //    return View();
            //}
        }
    }
}