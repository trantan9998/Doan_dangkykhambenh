using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using doan_qldkonline.Models;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;

namespace doan_qldkonline.Controllers
{
    public class datlichhenController : Controller
    {
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();

        // GET: datlichhen
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Noiquy()
        {
            return View();
        }
        public ActionResult thongtindangky()
        {     
            return View();
        }
        [HttpGet]
        public ActionResult capnhapthongtin()
        {
            ViewBag.email = Session["email"].ToString();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult capnhapthongtin([Bind(Exclude = "id_benhnhan")]datlichkham dlk, FormCollection f)
        {
           
            var email = Session["email"].ToString();
            var v = db.datlichkhams.Where(a => a.email == email).FirstOrDefault();
            v.ngaysinh = DateTime.Parse(f["ngaysinh"].ToString());
            v.gioitinh = f["gioitinh"].ToString();
            db.Entry(v).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("capnhapthongtin");
            

        }


        [HttpGet]
        public ActionResult datlichhen()
        {
            ViewBag.email = Session["email"].ToString();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult datlichhen([Bind(Exclude = "id_benhnhan")]datlichkham dlk, FormCollection f)
        {
            //Session["email"] = v.email;
            //Session["hovaten"] = v.hoten;
            //Session["sodienthoai"] = v.sodienthoai;
            //Session["diachi"] = v.diachi;
            var email = Session["email"].ToString();
            var bacsi_id = int.Parse(Session["id_bacsi"].ToString());

            var v = db.datlichkhams.Where(a => a.email == email).FirstOrDefault();
            v.motatrieuchung = f["motatrieuchung"].ToString();
            v.ngaykham = DateTime.Parse(f["ngaykham"].ToString());
            v.giokham = f["giokham"].ToString();
            v.id_bacsi = bacsi_id;
            db.Entry(v).State = System.Data.Entity.EntityState.Modified;
            ViewBag.message = "ĐĂNG KÝ THÀNH CÔNG";
            db.SaveChanges();
            return RedirectToAction("datlichhen");
        }


    }
}