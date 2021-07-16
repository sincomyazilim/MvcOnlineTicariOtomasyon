using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class CariPanelController : Controller
    {
        // GET: CariPanel
       
        Context c = new Context();
      
        public ActionResult Index()
        {
            List<SelectListItem> deger1 = (from x in c.Ils.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.Adi,
                                               Value = x.ilId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var mail = (string)Session["CariMail"];
            var degerler = c.Caris.Where(x => x.CariMail == mail).ToList();
            var cariId=0;
            foreach (var x in degerler)
            {
                cariId = x.CariId;
            }
            var toplamislem = c.SatisHarekets.Where(x => x.CariId == cariId).Count();
            ViewBag.toplamislem = toplamislem;
            //ViewBag.toplam = c.SatisHarekets.Where(x => x.CariId == cariId).Sum(y => y.ToplamTutar).ToString();
            ViewBag.urunSayisi = c.SatisHarekets.Where(x => x.CariId == cariId).Sum(y => y.Adet);
            return View(degerler);
        }
        
        public ActionResult Siparislerim()
        {
             var mail= (string)Session["CariMail"];
             var id = c.Caris.Where(x => x.CariMail == mail.ToString()).Select(y=>y.CariId).FirstOrDefault();
             var satislerim = c.SatisHarekets.Where(x => x.CariId == id).OrderByDescending(x=>x.SatisId).ToList();
           var caribilgi =c.Caris.Where(x => x.CariMail == mail.ToString()).FirstOrDefault();
            @ViewBag.dCari = caribilgi.CariAd.ToString() + " " + caribilgi.CariSoyad.ToString();
            return View(satislerim);
        }

        public ActionResult Faturalarim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Caris.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();
            var satislerim = c.SatisHarekets.Where(x => x.CariId == id&& x.Durum).ToList();
            var caribilgi = c.Caris.Where(x => x.CariMail == mail.ToString()).FirstOrDefault();
            @ViewBag.dCari = caribilgi.CariAd.ToString() + " " + caribilgi.CariSoyad.ToString();
            return View(satislerim);
        }

        public ActionResult Kargolarim()
        {
            var mail = (string)Session["CariMail"];
            var caribilgi = c.Caris.Where(x => x.CariMail == mail.ToString()).FirstOrDefault();
            var sorgu = c.KargoDetays.Where(x => x.CariId == caribilgi.CariId).ToList();
            return View(sorgu);
        }
        public ActionResult KargoTesliAldim(int id)
        {
            var sorgu = c.KargoDetays.Find(id);
            sorgu.Durum = true;
            c.SaveChanges();
            return RedirectToAction("Kargolarim");

        }
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            

            var mesajlar = c.Mesajs.Where(x=>x.Alici==mail).OrderByDescending(x=>x.MesajId).ToList();
            GelenGidenMesajSayisi();
            return View(mesajlar);
        }
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajs.Where(x => x.Gönderici == mail).OrderByDescending(x => x.MesajId).ToList();
         
            GelenGidenMesajSayisi();
            return View(mesajlar);
        }
        public void GelenGidenMesajSayisi()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar1 = c.Mesajs.Where(x => x.Alici == mail).ToList();
            ViewBag.gelenMesajSayisi = mesajlar1.Count();
            var mesajlar = c.Mesajs.Where(x => x.Gönderici == mail).ToList();
            ViewBag.gidenMesajSayisi = mesajlar.Count();

        }

        public ActionResult MesajDetay(int id)
        {
            var sorgu = c.Mesajs.Where(x=>x.MesajId==id).ToList();
            GelenGidenMesajSayisi();
            return View(sorgu);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            GelenGidenMesajSayisi();
        
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(Mesaj mesaj)
        {
            var mail = (string)Session["CariMail"];
            mesaj.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            mesaj.Gönderici = mail;
            c.Mesajs.Add(mesaj);

            c.SaveChanges();
            return RedirectToAction("GelenMesajlar");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
        public PartialViewResult Partial()
        {
            List<SelectListItem> deger1 = (from x in c.Ils.ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.Adi,
                                               Value = x.ilId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var mail = (string)Session["CariMail"];
            var caribul = c.Caris.Where(x => x.CariMail == mail).FirstOrDefault();

            return PartialView("Partial",caribul);
        }
        [HttpPost]
        public ActionResult CariGuncelle(Cari cari)
        {
            var CariUpdate = c.Caris.Find(cari.CariId);
            CariUpdate.CariAd = cari.CariAd;
            CariUpdate.CariSoyad = cari.CariSoyad;
            CariUpdate.ilId = cari.ilId;
            // CariUpdate.CariMail = cari.CariMail;
            CariUpdate.CariSifre = cari.CariSifre;
            CariUpdate.VergiNo = cari.VergiNo;
            CariUpdate.TcNo = cari.TcNo;
            CariUpdate.Adres = cari.Adres;
            CariUpdate.Durum = cari.Durum;

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Urun(string p, int sayfa = 1)
        {
            var urunler = from x in c.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(y => y.UrunAd.Contains(p));
            }
            return View(urunler.ToList().ToPagedList(sayfa, 10));
        }

        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Personels.Where(x => x.DepartmanId == 1 || x.DepartmanId == 2).ToList()//droplistdown dolduruyoruz
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var mail = (string)Session["CariMail"];
            var cari = c.Caris.Where(x => x.CariMail == mail).FirstOrDefault();
            ViewBag.cariId = cari.CariId;

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

            return RedirectToAction("Siparislerim", "CariPanel");
        }
    }
}