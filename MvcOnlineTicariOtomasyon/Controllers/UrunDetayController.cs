using MvcOnlineTicariOtomasyon.Models.Siniflar;
using MvcOnlineTicariOtomasyon.Models.Siniflar.Context;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A")]
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context c = new Context();
        public ActionResult Index()
        {
            UrunDetay UD = new UrunDetay();

            UD.Deger1 = c.Uruns.Where(x => x.UrunId == 1).ToList();
            UD.Deger2 = c.Detays.Where(x => x.DetayId == 4).ToList();
            return View(UD);
        }
    }
}