using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult KayıtOl()
        {
            List<SelectListItem> deger1 = (from x in c.Ils.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.Adi,
                                               Value = x.ilId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            return PartialView();
        }
        [HttpPost]
        public PartialViewResult KayıtOl(Cari cari)
        {
            c.Caris.Add(cari);
            c.SaveChanges();
            return PartialView();
        }
      
        [HttpGet]
        public ActionResult CariGiris()
        {
           
            return PartialView();
        }
        [HttpPost]
        public ActionResult CariGiris(Cari cari)
        {
            var sorgu = c.Caris.FirstOrDefault(x => x.CariMail == cari.CariMail && x.CariSifre == cari.CariSifre);
            if (sorgu!=null)
            {
                FormsAuthentication.SetAuthCookie(sorgu.CariMail, false);
                Session["CariMail"] = sorgu.CariMail.ToString();
                return RedirectToAction("Index","CariPanel");

            }
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AdminLogin(Personel per)//Perseonle tablosu uzerınde yetkılendırme yapacaz
        {
            var adminBilgi = c.Personels.FirstOrDefault(x => x.MAil == per.MAil && x.Sifre == per.Sifre);
            if (adminBilgi!=null)
            {
                FormsAuthentication.SetAuthCookie(adminBilgi.MAil, false);
                Session["Mail"] = adminBilgi.MAil.ToString();
                return RedirectToAction("Index", "istatistik");
            }
            return RedirectToAction("Index", "Login");
        }
    }
}