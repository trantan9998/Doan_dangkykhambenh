using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using doan_qldkonline.Models;
namespace doan_qldkonline.Controllers
{
    public class KhoaController : Controller
    {
        QL_DKKHAMBENH_ONLINEEntities db = new QL_DKKHAMBENH_ONLINEEntities();
        // GET: Khoa
        public ActionResult khoa()
        {
            return View();
        }
        public ActionResult khoanoi()
        {
            return View();
        }
        public ActionResult khoangoai()
        {
            return View();
        }
       
        public ActionResult bacsy()
        {
            return View(db.bacsis.ToList());
        }

        public ActionResult timkiem(string timkiem)
        {
            return View(db.bacsis.Where(x => x.kinhnghiem.Contains(timkiem) || timkiem == null).ToList());
        }
        public ActionResult chitietbacsi(int id_bs = 0)
        {
            bacsi bs = db.bacsis.SingleOrDefault(m => m.id_bacsi == id_bs);
            if (bs == null)
            {
                Response.StatusCode = 404;
                
                return null;
            }
            Session["id_bacsi"] = id_bs;
            return View(bs);
        }
        public ActionResult datlichkham_bs()
        {
            return View();
        }
    }
}