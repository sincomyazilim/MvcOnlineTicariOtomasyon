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
    public class istatistikController : Controller
    {
        // GET: istatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var cariler = c.Caris.Count().ToString();
            ViewBag.d1 = cariler;
            var urunler = c.Uruns.Count().ToString();
            ViewBag.d2 = urunler;
            var personeller = c.Personels.Count().ToString();
            ViewBag.d3 = personeller;
            var kategoriler = c.Kategoris.Count().ToString();
            ViewBag.d4 = kategoriler;

            var stoklar = c.Uruns.Sum(x=>x.Stok).ToString();
            ViewBag.d5 = stoklar;
            var markalarSayisi = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = markalarSayisi;
            var stokKritik = c.Uruns.Count(x => x.Stok<=10).ToString();
            ViewBag.d7 = stokKritik;
            var maxFiyatliUrun = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = maxFiyatliUrun;


            var minFiyatliUrun = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = minFiyatliUrun;
            var maxMarka = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d10 = maxMarka;
            var buzdolabiSayisi = c.Uruns.Count(x => x.UrunAd == "Buzdolabi").ToString();
            ViewBag.d11 = buzdolabiSayisi;
            var latopSayisi = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d12 = latopSayisi;

            var dgr13 = c.Uruns.Where(u => u.UrunId == (c.SatisHarekets.GroupBy(x => x.UrunId)
            .OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.d13 = dgr13;
            var dgr14 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = dgr14;

            DateTime bugun = DateTime.Today;
            var dgr15 = c.SatisHarekets.Count(x => x.Tarih==bugun).ToString();
            if (dgr15==null)
            {
                ViewBag.d15 = "0";
            }
            ViewBag.d15 = dgr15;

            //var dgr16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(x =>(decimal)x.ToplamTutar).ToString();

            //if (dgr16 == null)
            //{
            //    ViewBag.d16 = "0,00";
            //}
            //ViewBag.d16 = dgr16;
            return View();
        }

        public ActionResult KolayTablolar()
        {
            var sehirGrupla = (from x in c.Caris join k in c.Ils on x.ilId equals k.ilId
                               group x by k.Adi into g
                               select new GrupSinif
                               {
                                   Sehir = g.Key,
                                   Sayi=g.Count()

                               }).ToList() ;
            return View(sehirGrupla);
        }

        public PartialViewResult Partial1()
        {
            var sorgu = (from x in c.Personels
                        group x by x.Departman.DepartmanAd into g
                        select new GrupSinif2
                        { 
                            Departman = g.Key,
                            Sayi = g.Count()

                        }).ToList();
            return PartialView(sorgu);
        }

        public PartialViewResult Partial2()
        {
            var sorgu = c.Caris.ToList();

            return PartialView(sorgu);
        }

        public PartialViewResult Partial3()
        {
            var sorgu = c.Uruns.ToList();

            return PartialView(sorgu);
        }

        public PartialViewResult Partial4()
        {
            var sorgu = (from x in c.Uruns
                         group x by x.Marka into g
                         select new GrupSinifi3
                         {
                               Marka = g.Key,
                              Sayi= g.Count()

                         }).ToList();
            return PartialView(sorgu);
        }
    }
}