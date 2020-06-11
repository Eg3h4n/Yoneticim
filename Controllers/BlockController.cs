using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yoneticim.Controllers
{
    public class BlockController : Controller
    {
        YoneticimDBEntities db = new YoneticimDBEntities();

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult List()
        {
            // Sadece binaları listele..
            return View(db.Mulklers.Where(x => x.MulkId == null && x.BlokSayisi == 1).ToList());
        }

        public ActionResult Details(int id)
        {
            // daireleri listele..
            Mulkler bina = db.Mulklers.FirstOrDefault(x => x.Id == id);

            return View(bina.Dairelers.ToList());
        }

        public ActionResult Detail(int id)
        {
            return View();
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