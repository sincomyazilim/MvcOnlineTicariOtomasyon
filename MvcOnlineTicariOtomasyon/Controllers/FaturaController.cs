using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    
    public class FaturaController : Controller
    {
        // GET: Fatura

        Context c = new Context();
        string genelId;
        public ActionResult Index()
        {
            var faturaListesi = c.Faturas.Where(x => x.Durum).ToList();
            return View(faturaListesi);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Fatura fatura)
        {
            c.Faturas.Add(fatura);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaIptal(int id)
        {
            var faturaIptal = c.Faturas.Find(id);

            faturaIptal.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturas.Find(id);
            return View("FaturaGetir", fatura);

        }

        [HttpPost]
        public ActionResult FaturaGuncelle(Fatura fatura)
        {
            var faturaGuncelle = c.Faturas.Find(fatura.FaturaId);
            faturaGuncelle.FaturaSeriNo = fatura.FaturaSeriNo;
            faturaGuncelle.FaturaSiraNo = fatura.FaturaSiraNo;
            faturaGuncelle.Tarih = fatura.Tarih;
            faturaGuncelle.Saat = fatura.Saat;
            faturaGuncelle.VergiDairesi = fatura.VergiDairesi;
            faturaGuncelle.VergiNo = fatura.VergiNo;
            faturaGuncelle.TeslimEden = fatura.TeslimEden;
            faturaGuncelle.TeslimAlan = fatura.TeslimAlan;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //--------------------------------------------------------------------FaturaKalems
        public ActionResult FaturaKalem(int id)
        {
            var faturakalemleri = c.FaturaKalems.Where(x => x.FaturaId == id).ToList();
             ViewBag.FaturaId = id;
           
            return View("FaturaKalem", faturakalemleri);
        }
        public ActionResult FaturaKalemGetir(int id)
        {
            var faturaKalem = c.FaturaKalems.Find(id);
            return View("FaturaKalemGetir", faturaKalem);

        }

        [HttpPost]
        public ActionResult FaturaKalemGuncelle(FaturaKalem faturaKalem)
        {
            var faturaKalemGuncelle = c.FaturaKalems.Find(faturaKalem.FaturaKalemId);
            faturaKalemGuncelle.UrunAdi = faturaKalem.UrunAdi;
            faturaKalemGuncelle.Miktar = faturaKalem.Miktar;
            faturaKalemGuncelle.BirimFiyat = faturaKalem.BirimFiyat;
            faturaKalemGuncelle.Aciklama = faturaKalem.Aciklama;
            faturaKalemGuncelle.Tutar = faturaKalem.Tutar;
            faturaKalemGuncelle.FaturaId = faturaKalem.FaturaId;
            c.SaveChanges();
            return RedirectToAction("FaturaKalem/"+faturaKalem.FaturaId);
        }
        public ActionResult FaturaKalemSil(int id)
        {
            var faturaSil = c.FaturaKalems.Find(id);

            c.FaturaKalems.Remove(faturaSil);
            c.SaveChanges();
            return RedirectToAction("FaturaKalem/"+faturaSil.FaturaId);
        }

        [HttpGet]
      
        public ActionResult YeniFaturaKalem()
        {
            var routeValue = RouteData.Values["id"];
            genelId= routeValue.ToString();
            @ViewBag.GelenId= routeValue.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult YeniFaturaKalem(FaturaKalem faturaKalemEkle)
        {
            
            c.FaturaKalems.Add(faturaKalemEkle);
            c.SaveChanges();
            return RedirectToAction("FaturaKalem/" + faturaKalemEkle.FaturaId);
        }
      
     
    }
}