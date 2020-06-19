using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace doan_qldkonline.Models
{
    public class dangnhap
    {

        [DisplayName("email")]
        [EmailAddress(ErrorMessage = "Email Không Hợp Lệ")]
        [Required(ErrorMessage = "Email Không Bỏ Trống !")]
        public string email { get; set; }
        [DisplayName("Mật Khẩu")]
        [Required(ErrorMessage = "Mật Khẩu Bỏ Trống !")]
        public string matkhau { get; set; }

    }
}