using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A")]
    public class YapilacakController : Controller
    {
        // GET: Yapilacak
        Context c = new Context();
        public ActionResult Index()
        {
            var d1 = c.Caris.Count().ToString();
            ViewBag.d1 = d1;
            var d2 = c.Uruns.Count().ToString();
            ViewBag.d2 = d2;
            var d3 = c.Kategoris.Count().ToString();
            ViewBag.d3 = d3;
            var d4 = c.Personels.Count().ToString();
            ViewBag.d4 = d4;

            var sorgu = c.Yapilacaks.Where(x=>x.Durum).OrderBy(x=>x.saat).ToList();
            return View(sorgu);
        }
    }
}