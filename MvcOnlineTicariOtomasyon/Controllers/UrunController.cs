using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,U")]
    public class UrunController : Controller
    {
        Context c = new Context();
       
        public ActionResult Index(string p,int sayfa=1)
        {
            var urunler = from x in c.Uruns select x ;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(y => y.UrunAd.Contains(p));
            }
            return View(urunler.ToList().ToPagedList(sayfa, 10)) ;
        }
     
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
       
        [HttpPost]
        public ActionResult YeniUrun(Urun urun)
        {
            c.Uruns.Add(urun);
            
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult UrunStokVar(int id)
        { 
            var urunPasifeAl = c.Uruns.Find(id);
            urunPasifeAl.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Index");


        }
        [Authorize(Roles = "A")]
        public ActionResult UrunStokYok(int id)
        {
            var urunPasifeAl = c.Uruns.Find(id);
            urunPasifeAl.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");


        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult MiktarEkle(int id)
        {
            var model = c.Uruns.Find(id);
           
            return View("MiktarEkle", model);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult MiktarEkle(Urun urun)
        {
            var model = c.Uruns.Find(urun.UrunId);
            model.Stok += urun.Stok;
            
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult UrunGetir(int id)
        { List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult UrunGuncelle(Urun urun)
        {
            var urunUpdate = c.Uruns.Find(urun.UrunId);
            urunUpdate.UrunAd = urun.UrunAd;
            urunUpdate.Marka = urun.Marka;
            urunUpdate.Stok = urun.Stok;
            urunUpdate.AlisFiyat = urun.AlisFiyat;
            urunUpdate.SatisFiyat = urun.SatisFiyat;
          
            urunUpdate.UrunGorsel = urun.UrunGorsel;
            urunUpdate.Durum = urun.Durum;
            urunUpdate.KategoriId = urun.KategoriId;

            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi(Urun urun)
        {
            var urunListesi = c.Uruns.ToList();
            return View(urunListesi);
        }
       
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Personels.Where(x=>x.DepartmanId==1||x.DepartmanId==2).ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            //CariUrun cu = new CariUrun();
            //cu.SatisHareket = c.SatisHarekets.ToList();
            //cu.Cari = c.Caris.Where(x=>x.Durum).ToList();
           
            var idGonder = c.Uruns.Find(id);
            ViewBag.dgr2 = idGonder.UrunId;
            ViewBag.dgr3 = idGonder.SatisFiyat;

            return View();
        }

        [HttpPost]
        public ActionResult SatisYap(SatisHareket satisHareket)
        {
            satisHareket.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(satisHareket);
            c.SaveChanges();

            return RedirectToAction("Index", "Satis");
        }
    }
}