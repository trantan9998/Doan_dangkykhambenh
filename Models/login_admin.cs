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
        public int ma_user { get; set; }
        public string name_user { get; set; }
        public string password { get; set; }
        public Nullable<int> quyen { get; set; }
        public Nullable<int> id_bacsi { get; set; }

        public virtual bacsi bacsi { get; set; }
    }
}