using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yoneticim.Controllers
{
    public class StaffController : Controller
    {
        private YoneticimDBEntities db = new YoneticimDBEntities();

        public ActionResult List(int mulkid, int? gorevliTurId)
        {
            List<Gorevli> gorevliler = null;

            if (gorevliTurId == null)
                gorevliler = db.Mulkler.Find(mulkid).Gorevliler.ToList();
            else
                gorevliler = db.Mulkler.Find(mulkid).Gorevliler.Where(x => x.GorevliTurleriId == gorevliTurId).ToList();

            return View(gorevliler);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(int mulkid, Gorevli gorevli)
        {
            Mulk mulk = db.Mulkler.Find(mulkid);

            if (mulk != null)
            {
                mulk.Gorevliler.Add(gorevli);
                db.SaveChanges();
            }

            return RedirectToAction("Details", "Block", new { id = mulkid });
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}