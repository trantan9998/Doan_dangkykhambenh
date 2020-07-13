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
    public class QL_TaikhoanController : Controller
    {
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();

        // GET: ql_website/QL_Taikhoan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QL_Taikhoan()
        {
            return View();
        }
        public ActionResult ds_dangkybacsi()
        {
            return View(db.datlichkhams.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            datlichkham dkkb = db.datlichkhams.Find(id);
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
            datlichkham dk = db.datlichkhams.Find(id);
            if (dk == null)
            {
                return HttpNotFound();
            }
            ViewBag.bacsi = new SelectList(db.bacsis.ToList().OrderBy(n => n.id_bacsi), "id_bacsi", "id_bacsi");
            return View(dk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(datlichkham dlk)
        {
            if (ModelState.IsValid)
            {

                db.Entry(dlk).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ds_dangkybacsi");
            }
            ViewBag.bacsi = new SelectList(db.bacsis.ToList().OrderBy(n => n.id_bacsi), "id_bacsi", "id_bacsi");

            return View(dlk);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            datlichkham tt = db.datlichkhams.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return View(tt);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            datlichkham dlk = db.datlichkhams.SingleOrDefault(n => n.id_benhnhan == id);
            if (ModelState.IsValid)
            {
                db.datlichkhams.Remove(dlk);
                db.SaveChanges();
            }
            return RedirectToAction("ds_dangkybacsi");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult XuatFileExcel()
        {

            var dh = db.datlichkhams.ToList();
            var gv = new GridView();
            //===================================================
            DataTable dt = new DataTable();
            //Add Datacolumn
            dt.Columns.Add("mã bệnh nhân", typeof(string));
            dt.Columns.Add("họ và tên", typeof(string));
            dt.Columns.Add("Ngày sinh", typeof(string));
            dt.Columns.Add("giới tính", typeof(string));
            dt.Columns.Add("Số điện thoại", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("địa chỉ", typeof(string));
            dt.Columns.Add("mô tả triệu chứng", typeof(string));
            dt.Columns.Add("ngày khám", typeof(string));
            dt.Columns.Add("Giờ khám", typeof(string));
            //dt.Columns.Add("ngờ khám", typeof(string));
            
            foreach (var item in dh)
            {
                DataRow newRow = dt.NewRow();

                newRow["mã bệnh nhân"] = item.id_benhnhan;
                newRow["họ và tên"] = item.hoten;
                newRow["Ngày sinh"] = item.ngaysinh;
                newRow["giới tính"] = item.gioitinh;
                newRow["Số điện thoại"] = item.sodienthoai;
                newRow["Email"] = item.email;
                newRow["địa chỉ"] = item.diachi;
                newRow["mô tả triệu chứng"] = item.motatrieuchung;
                //newRow["giờ khám"] = item.giokham;
                newRow["ngày khám"] = item.ngaykham;
                newRow["Giờ khám"] = item.giokham;
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
            return Redirect("/ql_website/ds_dangkybacsi");
        }

    }
}