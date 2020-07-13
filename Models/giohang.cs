using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doan_qldkonline.Models
{
    public class giohang
    {
        QL_DKKHAMBENH_ONLINEEntities1 db = new QL_DKKHAMBENH_ONLINEEntities1();

        public int i_goikham { get; set; }

        public string ten_goikham { get; set; }

        public string hinhanh { get; set; }

        public double dongia { get; set; }

        public int soluong { get; set; }

        public double Thanhtien
        {
            get
            {
                return soluong * dongia;
            }
        }
        public giohang(int ma_hoadon)
        {
            i_goikham = ma_hoadon;
            goi_kham_suc_khoe gk = db.goi_kham_suc_khoe.Single(n => n.id_goikham == ma_hoadon);
            ten_goikham = gk.ten_goikham;
            hinhanh = gk.hinhanh;
            dongia = double.Parse(gk.gia.ToString());
            soluong = 1;
        }
    }
}