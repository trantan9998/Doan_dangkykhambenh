using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using doan_qldkonline.Models;
using System.Net;
using System.Net.Mail;

namespace doan_qldkonline.Areas.ql_website.Controllers
{
    public class EmailController : Controller
    {
        // GET: ql_website/Email
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(doan_qldkonline.Models.email model)
        {
            MailMessage mm = new MailMessage("trantan5984979999@gmail.com", model.To);
            mm.Subject = model.Subject;
            mm.Body = model.Body;
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("trantan5984979999@gmail.com", "0966375505");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.message = "GỬI EMAIL THÀNH CÔNG";
            return View();
        }
    }
}