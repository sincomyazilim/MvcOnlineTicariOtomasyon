using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    

    public class DepartmanController : Controller
    {
        Context c = new Context();
        [Authorize(Roles = "A")]
        public ActionResult Index()
        {
            var departmanlar = c.Departmans.Where(x => x.Durum == true).ToList();
          
            return View(departmanlar);
        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult DepartmanEkle(Departman departman)
        {
            c.Departmans.Add(departman);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult DepartmanSil(int id)
        {
            var departmanPasifAl = c.Departmans.Find(id);
            departmanPasifAl.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "A")]
        public ActionResult DepartmanGetir(int id)
        {
            var departman = c.Departmans.Find(id);

            return View("DepartmanGetir", departman);
        }
        [Authorize(Roles = "A")]
        public ActionResult DepartmanGuncelle(Departman departman)
        {
            var DepartmanUpdate = c.Departmans.Find(departman.DepartmanId);
            DepartmanUpdate.DepartmanAd = departman.DepartmanAd;

            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult DepartmanDetay(int id)
        {
            var personelList = c.Personels.Where(x => x.DepartmanId == id).ToList();
            var dpt = c.Departmans.Where(x => x.DepartmanId == id).Select(x => x.DepartmanAd).FirstOrDefault();
            ViewBag.dpt = dpt;
            return View(personelList);
        }
        [Authorize(Roles = "A,U")]
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var satisHareket = c.SatisHarekets.Where(x => x.PersonelId == id).OrderByDescending(x=>x.SatisId).ToList();
            ViewBag.dpersonel = c.Personels.Where(x => x.PersonelId == id).Select(x => x.PersonelAd +" "+ x.PersonelSoyad).FirstOrDefault();
            ViewBag.dpersonelId = id;
            return View(satisHareket);
        }
        [Authorize(Roles = "A")]
        public ActionResult PersonelSatisListesi(int id)
        {
            var personelSatisDetay = c.SatisHarekets.Where(x => x.PersonelId == id).ToList();
            return View(personelSatisDetay);
        }

    }
}