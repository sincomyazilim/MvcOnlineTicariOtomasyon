using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A")]
    public class QRController : Controller
    {
        // GET: QR
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string kod)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator kodured = new QRCodeGenerator();
                QRCodeGenerator.QRCode kareKod = kodured.CreateQrCode(kod,QRCodeGenerator.ECCLevel.Q);

                using (Bitmap resim = kareKod.GetGraphic(10))
                {
                    resim.Save(ms, ImageFormat.Png);
                    ViewBag.karekod = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();
        }
    }
}