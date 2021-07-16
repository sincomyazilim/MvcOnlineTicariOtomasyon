using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using System;
using PagedList;
using PagedList.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,U")]
    public class KargoController : Controller
    {
        // GET: Kargo

        Context c = new Context();
        public ActionResult Index(string p, int sayfa = 1)
        {
            var mail = (string)Session["Mail"];
            var personel = c.Personels.Where(x => x.MAil == mail).FirstOrDefault();
            
            
            
            if (personel.Yetki == "A")
            {
                var kargolar = from x in c.KargoDetays select x;
                if (!string.IsNullOrEmpty(p))
                {
                     kargolar = kargolar.Where(y => y.TakipKodu.Contains(p));
                }
                return View(kargolar.ToList().ToPagedList(sayfa, 10));

            }
            else
            {
             var kargolar = from x in c.KargoDetays.Where(x => x.PersonelId == personel.PersonelId) select x; 
            
                if (!string.IsNullOrEmpty(p))
                {
                     kargolar = kargolar.Where(y => y.TakipKodu.Contains(p));
                }
                return View(kargolar.ToList().ToPagedList(sayfa, 10));
            }
          

        }

        [HttpGet]
        public ActionResult KargoEkle(int id)
        {
            TakipKodUret();
           

           var sorgu = c.SatisHarekets.Find(id);
            ViewBag.satisId = id;
            ViewBag.urunId = sorgu.Urun.UrunAd;
            ViewBag.cariId = sorgu.CariId;
            ViewBag.adres = sorgu.Cari.Adres;
            ViewBag.personelId = sorgu.PersonelId;

            ViewBag.cariAd = sorgu.Cari.CariAd + " " + sorgu.Cari.CariSoyad;
            ViewBag.personelAd = sorgu.Personel.PersonelAd + " " + sorgu.Personel.PersonelSoyad;
            return View();
        }
        [HttpPost]
        public ActionResult KargoEkle(KargoDetay kargoDetay)
        {
            kargoDetay.Tarih= DateTime.Parse(DateTime.Now.ToShortDateString());
            var sorgu = c.SatisHarekets.Find(kargoDetay.SatisId);
            kargoDetay.Durum = false;
            c.KargoDetays.Add(kargoDetay);
            sorgu.KargoDurum = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        

        public void TakipKodUret()
        {
            Random rdn = new Random();
            string[] karekterler = { "A", "B", "C", "D", "E", "K", "M", "Z" };
            int k1, k2, k3;
            k1 = rdn.Next(0, karekterler.Length);
            k2 = rdn.Next(0, karekterler.Length);
            k3 = rdn.Next(0, karekterler.Length);
            int s1, s2, s3;
            s1 = rdn.Next(100, 1000);
            s2 = rdn.Next(10, 99);
            s3 = rdn.Next(10, 99);

            string kod = s1.ToString() + karekterler[k1] + s2.ToString() + karekterler[k2] + s3.ToString() + karekterler[k3];
            ViewBag.takipKodu = kod;
        }
      
    }
}