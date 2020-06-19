using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace doan_qldkonline.Models
{
    public class login_admin
    {
        [DisplayName("email")]
        [Required(ErrorMessage = "Vui lòng nhập Tài khoản")]
        public string name_user { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }
    }
}