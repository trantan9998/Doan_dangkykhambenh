using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using doan_qldkonline.Models;
using System.Web.Security;

namespace doan_qldkonline.Areas.ql_website.Controllers
{
    public class LoginController : Controller
    {
        // GET: ql_website/Login
        //sqlconnection con = new sqlconnection();
        //sqlcommand com = new sqlcommand();
        //sqldatareader dr;
        QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities();

        [HttpGet]
        public ActionResult index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult index(FormCollection f)
        {
            string taikhoan = f["txttaikhoan"].ToString();
            string matkhau = f["txtmatkhau"].ToString();
            LOGIN kh = db.LOGINs.SingleOrDefault(n => n.name_user == taikhoan && n.password == matkhau);
            if (kh != null)
            {
                Session["nguoidung"] = kh;
                Session["taikhoan"] = kh.name_user;
                ViewBag.thongbao = "đăng nhập thành công";
                return RedirectToAction("thongke", "Thongke");
            }
            ViewBag.thongbao1 = "tên tài khoản và mật khẩu không đúng!";
            return View();
        }
        public ActionResult dangxuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        //void conectionString()
        //{
        //    con.ConnectionString = "data source=VANTAN\TRANTAN; database=QL_DKKHAMBENH_ONLINE; integrated security=SSPI;";

        //}
        //[HttpPost]
        //public ActionResult Varify(Models.Account acc)
        //{
        //    conectionString();
        //    con.Open();
        //    com.Connection = con;
        //    com.CommandText = "select *from LOGIN where name_user='"+acc.Tendangnhap+"' and password='"+acc.Matkhau+"'";
        //    dr = com.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        con.Close();
        //        return View("index");
        //    }
        //    else
        //    {
        //        con.Close();
        //        return View();
        //    }
        //}

    }
}