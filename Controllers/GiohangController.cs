using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using doan_qldkonline.Models;

namespace doan_qldkonline.Controllers
{
    public class GiohangController : Controller
    { //lấy giỏ hàng 
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();
        public List<giohang> layhoadon()
        {
            List<giohang> lstgiohang = Session["giohang"] as List<giohang>;
            if (lstgiohang == null)
            {
                //nếu giỏ hàng chưa tồn tại thì mình tiến hàn khởi tạo
                lstgiohang = new List<giohang>();
                Session["giohang"] = lstgiohang;
            }
            return lstgiohang;
        }
        //thêm giỏ hàng 
        public ActionResult themgiohang(int i_gk, string strURL)
        {
            goi_kham_suc_khoe sp = db.goi_kham_suc_khoe.SingleOrDefault(n => n.id_goikham == i_gk);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy ra sesion giỏ hàng
            List<giohang> lstgiohang = layhoadon();
            //kiểm tra sản phẩm đã tồn tại trong sesion[giohang] chưa
            giohang goikham = lstgiohang.Find(n => n.i_goikham == i_gk);
            if (goikham == null)
            {
                goikham = new giohang(i_gk);
                //sản phẩm mới thêm vào list
                lstgiohang.Add(goikham);
                return Redirect(strURL);
            }
            else
            {
                goikham.soluong++;
                return Redirect(strURL);

            }
        }


        //cập nhập giỏ hàng
        public ActionResult capnhapgiohang(int i_gk, FormCollection f)
        {
            //kiểm tra mã sản phẩm
            goi_kham_suc_khoe sp = db.goi_kham_suc_khoe.SingleOrDefault(n => n.id_goikham == i_gk);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<giohang> lstgiohang = layhoadon();
            giohang gh = lstgiohang.SingleOrDefault(n => n.i_goikham == i_gk);
            //nếu mà tồn tại thì cho sửa giỏ hàng
            if (gh != null)
            {
                gh.soluong = int.Parse(f["txtsoluong"].ToString());
            }
            return RedirectToAction("giohang");
        }


        //xóa sản phẩm
        public ActionResult xoasanpham(int i_gk)
        {
            goi_kham_suc_khoe sp = db.goi_kham_suc_khoe.SingleOrDefault(n => n.id_goikham == i_gk);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy ra sesion giỏ hàng
            List<giohang> lstgiohang = layhoadon();
            giohang gh = lstgiohang.SingleOrDefault(n => n.i_goikham == i_gk);
            //nếu mà tồn tại thì cho sửa giỏ hàng
            if (gh != null)
            {
                lstgiohang.RemoveAll(n => n.i_goikham == i_gk);
            }
            if (lstgiohang.Count == 0)
            {
                return RedirectToAction("Trangchu", "Trangchu");
            }
            return RedirectToAction("giohang");
        }
        //xây dựng trang giỏ hàng
        public ActionResult giohang()
        {
            if (Session["giohang"] == null)
            {
                return RedirectToAction("Trangchu", "Trangchu");
            }
            List<giohang> lstgiohang = layhoadon();
            ViewBag.tongthanhtien = tongtien();
            return View(lstgiohang);
        }
        //xây dựng tính tổng số lượng và tổng tiền
        private int tongsoluong()
        {
            int itongsoluong = 0;
            List<giohang> lstgiohang = Session["giohang"] as List<giohang>;
            if (lstgiohang != null)
            {
                itongsoluong = lstgiohang.Sum(n => n.soluong);
            }
            return itongsoluong;
        }
        // tính tổng thành tiền
        private double tongtien()
        {
            double dtongtien = 0;
            List<giohang> lstgiohang = Session["giohang"] as List<giohang>;
            if (lstgiohang != null)
            {
                dtongtien = lstgiohang.Sum(n => n.Thanhtien);
            }
            return dtongtien;
        }

        // tổng số sản phẩm trong giỏ hàng
        public ActionResult giohangpartial()
        {
            if (tongsoluong() == 0)
            {
                return PartialView();
            }
            ViewBag.tongsoluong = tongsoluong();
            ViewBag.tongtien = tongtien();
            return PartialView();
        }
        //xây dựng 1 view cho user chỉnh sửa cho giỏ hàng
        public ActionResult suagiohang()
        {
            if (Session["giohang"] == null)
            {
                return RedirectToAction("Trangchu", "Trangchu");
            }
            List<giohang> lstgiohang = layhoadon();
            return View(lstgiohang);
        }

        //xây dựng chức năng đặt hàng
        public ActionResult dathang()
        {
            //kiểm tra đăng nhập
            if (Session["taikhoan"] == null || Session["taikhoan"].ToString() == "")
            {
                return RedirectToAction("login", "Taikhoan_user");
            }
            //kiểm tra giỏ hàng
            if (Session["giohang"] == null)
            {
                return RedirectToAction("giohang", "giohang");
            }
            //thêm đơn hàng
            HOADON dh = new HOADON();

            datlichkham kh = (datlichkham)Session["nguoidung"];
            List<giohang> gh = layhoadon();
            dh.id_hoadon = kh.id_benhnhan;
            dh.id_benhnhan = kh.id_benhnhan;
            dh.ngaydat = DateTime.Now;

            //thêm đơn hàng
            db.HOADONs.Add(dh);
            db.SaveChanges();
            //thêm chi tiết đơn hàng

            foreach (var item in gh)
            {
                Chitiet_HoaDon ctdh = new Chitiet_HoaDon();
                ctdh.id_goikham = item.i_goikham;
                ctdh.id_hoadon = dh.id_hoadon;
                ctdh.soluong = item.soluong;
                db.Chitiet_HoaDon.Add(ctdh);
                db.SaveChanges();

            }
            return RedirectToAction("ThongTinNguoiDung", "Home", new { @ID = Session["thongtin"] });

        }
    }
}