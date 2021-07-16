using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
  
    public class FaturaSatisController : Controller
    {
        // GET: FaturaSatis
        Context c = new Context();
        public ActionResult Index()
        {
            var sorgu = c.FaturaSatis.OrderByDescending(x=>x.FaturaId).ToList();
            return View(sorgu);
        }
        [HttpGet]
        public ActionResult FaturaEkle(int id)
        {
            List<SelectListItem> deger1 = (from x in c.VergiDaireleris.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.VergiDaire,
                                               Value = x.Id.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var sorgu = c.SatisHarekets.Where(x => x.SatisId == id).FirstOrDefault();
            var sorguCari = c.Caris.Where(x=>x.CariId==sorgu.CariId).FirstOrDefault();
            ViewBag.urunId = sorgu.UrunId;
            @ViewBag.satisId = id;
            @ViewBag.personelId = sorgu.PersonelId;
            @ViewBag.cariId = sorgu.CariId;
            @ViewBag.miktar = sorgu.Adet;
            @ViewBag.birimFiyat = sorgu.Fiyat;
            @ViewBag.tutar = sorgu.ToplamTutar;
            @ViewBag.tcNo = sorguCari.TcNo;
            @ViewBag.vergiNo = sorguCari.VergiNo;
            @ViewBag.CariAdi = sorguCari.CariAd + " " + sorguCari.CariSoyad;
            return View();

        }
        [HttpPost]
        public ActionResult FaturaEkle(FaturaSatis faturaSatis)
        {
            var sorgu = c.SatisHarekets.Where(x => x.SatisId == faturaSatis.SatisId).FirstOrDefault();
             faturaSatis.Tarih =DateTime.Parse(DateTime.Now.ToShortDateString());
            faturaSatis.Durum = true;
          
            c.FaturaSatis.Add(faturaSatis);
            sorgu.Durum = true;
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult FaturaIptal(int id)
        {
            var faturaIptal = c.FaturaSatis.Find(id);
            var sorgu = c.SatisHarekets.Find(faturaIptal.SatisId);
            sorgu.Durum = false;
            faturaIptal.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult FaturaGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.VergiDaireleris.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.VergiDaire,
                                               Value = x.Id.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var fatura = c.FaturaSatis.Find(id);
            return View("FaturaGetir", fatura);

        }

        [HttpPost]
        public ActionResult FaturaGuncelle(FaturaSatis fatura)
        {
            var faturaGuncelle = c.FaturaSatis.Find(fatura.FaturaId);
            faturaGuncelle.FaturaSeriNo = fatura.FaturaSeriNo;
            faturaGuncelle.FaturaSiraNo = fatura.FaturaSiraNo;
            faturaGuncelle.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            faturaGuncelle.Saat = fatura.Saat;
            faturaGuncelle.Id = fatura.Id;
            //faturaGuncelle.VergiNo = fatura.VergiNo;
            //faturaGuncelle.TcNo = fatura.TcNo;
            faturaGuncelle.Aciklama = fatura.Aciklama;
           
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}