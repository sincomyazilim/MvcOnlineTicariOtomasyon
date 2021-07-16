using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A")]
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context();
        public ActionResult Index(string p, int sayfa = 1)
        {
            var satislar = from x in c.SatisHarekets.OrderByDescending(x=>x.SatisId) select x;
            if (!string.IsNullOrEmpty(p))
            {
                satislar = satislar.Where(y => y.Cari.CariSoyad.Contains(p));
            }
            return View(satislar.ToList().ToPagedList(sayfa, 12));

            //var satisListesi = c.SatisHarekets.OrderBy(x=>x.Durum).ToList();
            //return View(satisListesi);
        }
       
        
        
        [HttpGet]
        public ActionResult YeniSatis()
        {
            DropdownDoldur();

            return View();
        }

        private void DropdownDoldur()
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.OrderBy(x => x.UrunAd).ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunId.ToString()
                                           }).ToList();


            List<SelectListItem> deger2 = (from x in c.Caris.OrderBy(x => x.CariAd).ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariId.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.Where(x => x.DepartmanId == 1 || x.DepartmanId == 2).ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
        }


        [HttpPost]
        public ActionResult YeniSatis(SatisHareket satisHareket)
        {
            satisHareket.Durum = false;
            satisHareket.KargoDurum = false;
            satisHareket.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(satisHareket);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult SatisGetir(int id)
        {
            DropdownDoldur();
            var satisDoldur = c.SatisHarekets.Find(id);
            return View("SatisGetir", satisDoldur);
        }
        
        [HttpPost]
        public ActionResult SatisGuncelle(SatisHareket satisHareket)
        {
            var satisUpdate = c.SatisHarekets.Find(satisHareket.SatisId);
            satisUpdate.UrunId = satisHareket.UrunId;
            satisUpdate.CariId = satisHareket.CariId;
            satisUpdate.PersonelId = satisHareket.PersonelId;
            satisUpdate.Adet = satisHareket.Adet;
            satisUpdate.Fiyat = satisHareket.Fiyat;
            satisUpdate.ToplamTutar = satisHareket.ToplamTutar;
            satisUpdate.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString()); ;

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisDetay(int id)
        {
            var satisDetay = c.SatisHarekets.Where(x => x.SatisId == id).ToList();
            return View(satisDetay);
        }

      
    }
}