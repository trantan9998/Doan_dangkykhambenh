﻿using System;
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
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();

        [HttpGet]
        public ActionResult index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult index(FormCollection f,login_admin lg)
        {
            //string taikhoan = f["txttaikhoan"].ToString();
            //string matkhau = f["txtmatkhau"].ToString();
            //LOGIN kh = db.LOGINs.SingleOrDefault(n => n.name_user == taikhoan && n.password == matkhau);
            //if (kh != null)
            //{
            //    Session["nguoidung"] = kh;
            //    Session["taikhoan"] = kh.name_user;
            //    ViewBag.thongbao = "đăng nhập thành công";
            //    return RedirectToAction("thongke", "Thongke");
            //}
            //ViewBag.thongbao1 = "tên tài khoản và mật khẩu không đúng!";
            //return View();
            {

                if (ModelState.IsValid)
                {
                    using (QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1())
                    {
                        var log = db.LOGINs.Where(a => a.name_user.Equals(lg.name_user) && a.password.Equals(lg.password)).FirstOrDefault();
                        if (log != null)
                        {
                            var getNameAccount = db.bacsis.Where(n => n.id_bacsi == log.id_bacsi).FirstOrDefault();

                            if (getNameAccount != null)
                            {
                                var getbs = db.bacsis.Where(n => n.id_bacsi == getNameAccount.id_bacsi).FirstOrDefault();
                                Session["getbacsi"] = getbs.id_bacsi.ToString();

                                Session["Admin"] = getNameAccount.hovaten;
                                //Session["taikhoan"] = lg.name_user;
                                Session["quyen"] = log.quyen;
                                return RedirectToAction("thongke", "thongke");
                            }
                            else
                            {
                                Session["Admin"] = "Admin";

                                Session["quyen"] = log.quyen;

                                return RedirectToAction("thongke", "thongke");

                            }

                        }
                        else
                        {
                            ViewBag.SuccessMessage = "Sai Email hoặc password !";
                        }
                    }
                }
                return View();


            }
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