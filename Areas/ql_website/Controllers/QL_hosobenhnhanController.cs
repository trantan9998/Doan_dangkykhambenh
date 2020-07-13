using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using doan_qldkonline.Models;

namespace doan_qldkonline.Areas.ql_website.Controllers
{
    public class QL_hosobenhnhanController : Controller
    {
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();
        // GET: ql_website/QL_hosobenhnhan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dsdangky_online()
        {
            return View(db.dangkykhambenhs.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dangkykhambenh dkkb = db.dangkykhambenhs.Find(id);
            if (dkkb == null)
            {
                return HttpNotFound();
            }
            return View(dkkb);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dangkykhambenh dk = db.dangkykhambenhs.Find(id);
            if (dk == null)
            {
                return HttpNotFound();
            }
            return View(dk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(dangkykhambenh dangkykhambenhh)
        {
            if (ModelState.IsValid)
            {
              
                db.Entry(dangkykhambenhh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("dsdangky_online");
            }
            return View(dangkykhambenhh);
        }
     

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dangkykhambenh tt = db.dangkykhambenhs.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            dangkykhambenh dlk = db.dangkykhambenhs.SingleOrDefault(n => n.id_benhnhan == id);
            if (ModelState.IsValid)
            {
                db.dangkykhambenhs.Remove(dlk);
                db.SaveChanges();
            }
            return RedirectToAction("dsdangky_online");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult timkiem(string timkiem)
        {
            return View(db.dangkykhambenhs.Where(x => x.sodienthoai.Contains(timkiem) || timkiem == null).ToList());
        }
        public ActionResult XuatFileExcel()
        {

            var dh = db.dangkykhambenhs.ToList();
            var gv = new GridView();
            //===================================================
            DataTable dt = new DataTable();
            //Add Datacolumn
            dt.Columns.Add("mã bệnh nhân", typeof(string));
            dt.Columns.Add("họ và tên", typeof(string));
            dt.Columns.Add("năm sinh", typeof(string));
            dt.Columns.Add("giới tính", typeof(string));
            dt.Columns.Add("địa chỉ", typeof(string));     
            dt.Columns.Add("Số điện thoại", typeof(string));     
            dt.Columns.Add("ngày khám", typeof(string));
            //dt.Columns.Add("ngờ khám", typeof(string));
            dt.Columns.Add("mô tả triệu chứng", typeof(string));
            dt.Columns.Add("Khác", typeof(string));

            foreach (var item in dh)
            {
                DataRow newRow = dt.NewRow();

                newRow["mã bệnh nhân"] = item.id_benhnhan;
                newRow["họ và tên"] = item.hovaten;
                newRow["năm sinh"] = item.namsinh;
                newRow["giới tính"] = item.gioitinh;
                newRow["địa chỉ"] = item.diachi;
                newRow["số điện thoại"] = item.sodienthoai;
                newRow["ngày khám"] = item.ngaykham;
                //newRow["giờ khám"] = item.giokham;
                newRow["mô tả triệu chứng"] = item.motatrieuchung;
                newRow["Khác"] = item.khac;


                dt.Rows.Add(newRow);
            }
            gv.DataSource = dt;
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment; filename=danh-sach.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            objHtmlTextWriter.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return Redirect("/ql_website/dsdangky_online");
        }

        


    }
}