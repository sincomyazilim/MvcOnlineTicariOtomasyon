using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,U")]
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var mail = (string)Session["Mail"];
            
            var personel = c.Personels.Where(x => x.MAil == mail).ToList();
            foreach (var item in personel)
            {
                if (item.Yetki=="A")
                {
                    var personeller = c.Personels.ToList();
                    return View(personeller);
                }

            }
            return View(personel);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;


            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel personel )
        {
            if (Request.Files.Count>0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.PersonelGorsel = "/Image/" + dosyaAdi + uzanti;
            }
            c.Personels.Add(personel);
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;


            var personel = c.Personels.Find(id);

            return View("PersonelGetir", personel);
        }

        public ActionResult PersonelGuncelle(Personel personel)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.PersonelGorsel = "/Image/" + dosyaAdi + uzanti;
            }
            var PersonelUpdate = c.Personels.Find(personel.PersonelId);
            PersonelUpdate.PersonelAd = personel.PersonelAd;
            PersonelUpdate.PersonelSoyad = personel.PersonelSoyad;
            PersonelUpdate.PersonelGorsel = personel.PersonelGorsel;
            PersonelUpdate.DepartmanId = personel.DepartmanId;

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelListesi()
        {
            var sorgu = c.Personels.ToList();
            return View(sorgu);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}