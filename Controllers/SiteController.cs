using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yoneticim.Controllers
{
    public class SiteController : Controller
    {
        YoneticimDBEntities db = new YoneticimDBEntities();

        [HttpPost]
        public ActionResult Add(string siteName, int blockCount, string address)
        {
            Mulkler mulk = new Mulkler()
            {
                Adi = siteName,
                Adres = address,
                BlokSayisi = blockCount
            };

            db.Mulklers.Add(mulk);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(int siteId, string siteName, int blockCount, string address)
        {
            Mulkler mulk = db.Mulklers.FirstOrDefault(x => x.Id == siteId);

            mulk.Adi = siteName;
            mulk.Adres = address;
            mulk.BlokSayisi = blockCount;

            db.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int siteId)
        {
            Mulkler mulk = db.Mulklers.FirstOrDefault(x => x.Id == siteId);
            db.Mulklers.Remove(mulk);

            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            // Sadece siteleri listele..
            return View(db.Mulklers.Where(x => x.MulkId == null && x.BlokSayisi > 1).ToList());
        }

        public ActionResult Details(int id)
        {
            // Sitenin binaları getirilir.
            Mulkler site = db.Mulklers.FirstOrDefault(x => x.Id == id);

            return View(site.Mulkler1.ToList());
        }

        public JsonResult GetSiteInfo(int id)
        {
            // sşte bilgisini json olarak verir.
            Mulkler site = db.Mulklers.FirstOrDefault(x => x.Id == id);

            return Json(site, JsonRequestBehavior.AllowGet);
        }
    }
}