using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using doan_qldkonline.Models;

namespace doan_qldkonline.Models
{
    public class Dangkykham
       
    {
        [HiddenInput(DisplayValue = false)]
        public int id_benhnhan { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "vui lòng nhập họ tên")]
        public string hoten { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "vui lòng nhập ngày sinh")]
        public string ngaysinh { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "vui lòng nhập giới tính")]

        public string gioitinh { get; set; }

        [Required(ErrorMessage = "vui lòng nhập số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        public string sodienthoai { get; set; }



        [Required(ErrorMessage = "vui lòng nhập Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }



        [Required(ErrorMessage = "vui lòng nhập địa chỉ")]
        public string diachi { get; set; }



        [Required(ErrorMessage = "vui lòng nhập mô tả triệu chứng")]
        [DataType(DataType.Text)]
        public string motatrieuchung { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ngaykham { get; set; }



        public int id_hoso { get; set; }


    

        [DataType(DataType.Upload)]
        public string hinhdaidien { get; set; }



        [Required(ErrorMessage = "vui lòng nhập mô tả triệu chứng")]
        [DataType(DataType.Password)]
        public string matkhau { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}