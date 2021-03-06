﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using doan_qldkonline.Models;
using System.Security;
namespace doan_qldkonline.Areas.ql_website.Controllers
{
    public class ThongkeController : Controller
    {
        // GET: ql_website/Thongke_benhnhan
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult taikhoanadmin()
        {
            return View(db.LOGINs.ToList());
        }
        public ActionResult thongke()
        {
            var tongSoDatLich = db.datlichkhams.OrderByDescending(model => model.id_benhnhan);
            ViewBag.tongSoDatLich = tongSoDatLich.Count();

            var tongSoBacSi = db.bacsis.OrderByDescending(model => model.id_bacsi);
            ViewBag.tongSoBacSi = tongSoBacSi.Count();

            var tongSohoso = db.dangkykhambenhs.OrderByDescending(model => model.id_benhnhan);
            ViewBag.tongSohoso = tongSohoso.Count();

            var tongSobaiviet = db.tintucs.OrderByDescending(model => model.id_tintuc);
            ViewBag.tongSobaiviet = tongSobaiviet.Count();

          
            return View();
        }
    }
}
