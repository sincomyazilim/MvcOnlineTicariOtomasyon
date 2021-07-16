using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,U")]
   
    public class CariController : Controller
    {
        // GET: Cari

        Context c = new Context();
        public ActionResult Index()
        {
            var cariList = c.Caris.OrderBy(x=>x.CariAd).ToList();
            return View(cariList);
        }

        [HttpGet]
        public ActionResult YeniCari()
        {
            List<SelectListItem> deger1 = (from x in c.Ils.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.Adi,
                                               Value = x.ilId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cari cari)
        {
            c.Caris.Add(cari);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult CariPasif(int id)
        {
            var cariPasifAl = c.Caris.Find(id);
            cariPasifAl.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult CariAktif(int id)
        {
            var cariPasifAl = c.Caris.Find(id);
            cariPasifAl.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult CariGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Ils.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.Adi,
                                               Value = x.ilId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var cari = c.Caris.Find(id);

            return View("CariGetir", cari);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult CariGuncelle(Cari cari)
        {
            var CariUpdate = c.Caris.Find(cari.CariId);
            CariUpdate.CariAd = cari.CariAd;
            CariUpdate.CariSoyad = cari.CariSoyad;
            CariUpdate.ilId = cari.ilId;
            // CariUpdate.CariMail = cari.CariMail;
            CariUpdate.CariSifre = cari.CariSifre;

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSatis(int id)
        {
            var musteriSatisListesi = c.SatisHarekets.Where(x => x.CariId == id).ToList();
            ViewBag.dCari = c.Caris.Where(x => x.CariId == id).Select(x => x.CariAd + " " + x.CariSoyad).FirstOrDefault();
            ViewBag.dCariId = id;
            return View(musteriSatisListesi);
        }

        public ActionResult CariSatisListesi(int id)
        {
            var cariSatisDetay = c.SatisHarekets.Where(x => x.CariId == id).ToList();
            return View(cariSatisDetay);
        }
    }
}