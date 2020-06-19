using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using doan_qldkonline.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;
namespace doan_qldkonline.Controllers
{
   
    public class Taikhoan_userController : Controller
    {
        QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities();
        // GET: Taikhoan_user
        public ActionResult thongtin_username()
        {
            return View();
        }
      
        [HttpGet]
        public ActionResult DangKi()
        {
            ViewBag.bacsi = new SelectList(db.bacsis.ToList().OrderBy(n => n.id_bacsi), "id_bacsi", "id_bacsi");

            return View();
        }
        [HttpPost]
         [ValidateAntiForgeryToken] // ktra validation
        public ActionResult DangKi([Bind(Exclude = "IsEmailVerified,ActivationCode")] datlichkham dlk, FormCollection f)
        {
            string message = "";
            //dlk.hoten = f["hotens"];
            //dlk.sodienthoai = f["sodienthoais"];
            //dlk.email = f["emails"];
            //dlk.matkhau = f["matkhaus"];
            //dlk.diachi = f["diachis"];
            //dlk.motatrieuchung = f["motatrieuchungs"];
            //dlk.ngaykham = DateTime.Parse(f["ngaykhams"].ToString());
            //dlk.giokham = f["giokhams"];
            //dlk.id_bacsi = int.Parse(Session["id_bacsi"].ToString());
           
            // Model Validation 
            bool Status = false;

            if (ModelState.IsValid)
            {

                //Email is already Exist 
                var isExist = IsEmailExist(dlk.email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Địa chỉ Email của bạn đã tồn tại");
                    return View(dlk);
                }


                #region Generate Activation Code 
                dlk.ActivationCode = Guid.NewGuid();
                #endregion

                #region  Password Hashing 
                dlk.matkhau = Hashing.Hash(dlk.matkhau);

                #endregion
                dlk.IsEmailVerified = false;

                #region Save to Database
                using (QL_DKKHAMBENH_ONLINEEntities dc = new QL_DKKHAMBENH_ONLINEEntities())
                {
                    dc.datlichkhams.Add(dlk);
                    dc.SaveChanges();

                    //Send Email to User
                    SendVerificationLinkEmail(dlk.email, dlk.ActivationCode.ToString());
                    message = "THÔNG TIN CỦA BẠN ĐÃ ĐƯỢC GỬI ĐI." +
                        " Vui lòng kiểm tra mail để kích hoạt thông tin đăng ký EMAIL: " +dlk.email;
                    Status = true;
                }
                #endregion
            }
            else
            {
                message = "VUI LÒNG KIỂM TRA LẠI THÔNG TIN";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            ViewBag.bacsi = new SelectList(db.bacsis.ToList().OrderBy(n => n.id_bacsi), "id_bacsi", "id_bacsi");
            return View(dlk);
        }
        

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities())
            {
                db.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                                                                // Confirm password does not match issue on save changes
                var v = db.datlichkhams.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    db.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }



        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(dangnhap dlk, string ReturnUrl = "")
        {
            //var IsAdmin = db.datlichkhams.SingleOrDefault(x => x.email == dlk.email);
            //if (IsAdmin != null)
            //{
            //    if (Hashing.ValidatePassword(dlk.matkhau, IsAdmin.matkhau))
            //    {
            //        Session["admin"] = IsAdmin.email;
            //        return View("thongtin_benhnhan");
            //    }

            //}
            //else
            //    ViewBag.thongbao = "😒 vui lòng kiểm tra lại tài khoản!";
            //return View();
            string message = "";
            using (QL_DKKHAMBENH_ONLINEEntities dc = new QL_DKKHAMBENH_ONLINEEntities())
            {
                var v = dc.datlichkhams.Where(a => a.email == dlk.email).FirstOrDefault();
                if (v != null)
                {
                    if (!v.IsEmailVerified)
                    {
                        ViewBag.Message = "Vui lòng xác minh email của bạn trước";
                        return View();
                    }

                    if (string.Compare(Hashing.Hash(dlk.matkhau), v.matkhau) == 0)
                    {
                        //int timeout = dlk.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        //var ticket = new FormsAuthenticationTicket(dlk.email, dlk.RememberMe, timeout);
                        //string encrypted = FormsAuthentication.Encrypt(ticket);
                        //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        //cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        //cookie.HttpOnly = true;
                        //Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("thongtin_username", "Taikhoan_user");
                        }
                    }
                    else
                    {
                        message = "Thông tin không hợp lệ";
                    }
                }
                else
                {
                    message = "Thông tin không hợp lệ";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "Taikhoan_user");
        }


        [HttpGet]
        public ActionResult thongtin_benhnhan()
        {
            //datlichkham hs = new datlichkham();

            //using (QL_DKKHAMBENH_OLINEEntities8 db = new QL_DKKHAMBENH_ONLINEEntities8())
            //{
            //    hs = db.datlichkhams.Where(x => x.id_benhnhan == id).FirstOrDefault();
            //}
            return View();
            //return View(db.Hosobenhnhans.ToList());
        }
        [NonAction]
        public bool IsEmailExist(string email)
        {
            using (QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities())
            {
                var v = db.datlichkhams.Where(a => a.email == email).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]

        private void SendVerificationLinkEmail(string email, object activationCode)
        {
            var verifyUrl = "/Taikhoan_user/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("trantan5984979999@gmail.com", "Bệnh viện ABC");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "0966375505"; // Replace with actual password
            string subject = "TÀI KHOẢN CỦA BẠN ĐÃ ĐƯỢC ĐĂNG KÝ THÀNH CÔNG!";

            string body = "<br/><br/>BẠN ĐÃ ĐĂNG KÝ THÀNH CÔNG KHÁM BỆNH TẠI BỆNH VIỆN ABC " +
                "<br/> BẠN VUI LÒNG NHẤP VÀO LIÊN KẾT DƯỚI ĐÂY ĐỂ XÁC MINH TÀI KHOẢN CỦA BẠN" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }


    }
}