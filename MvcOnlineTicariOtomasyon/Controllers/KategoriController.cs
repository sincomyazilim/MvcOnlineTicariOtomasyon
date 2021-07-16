using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using PagedList;
using PagedList.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A")]
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();

        public ActionResult Index(int sayfa=1)
        {
            var degerler = c.Kategoris.Where(x=>x.Durum).ToList().ToPagedList(sayfa,8);

            return View(degerler);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            c.Kategoris.Add(kategori);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            
               var kategoriPasifAl = c.Kategoris.Find(id);
            //c.Kategoris.Remove(kategoriPasifAl);
            
                kategoriPasifAl.Durum = false;
               
            
           

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult KategoriGetir(int id)
        {
            var kategori = c.Kategoris.Find(id);

            return View("KategoriGetir",kategori);
        }
        [HttpPost]
        public ActionResult KategoriGuncelle(Kategori kategori)
        {
            var kategoriUpdate = c.Kategoris.Find(kategori.KategoriId);
            kategoriUpdate.KategoriAd = kategori.KategoriAd;
           
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}