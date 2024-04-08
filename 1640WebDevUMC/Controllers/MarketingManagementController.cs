using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1640WebDevUMC.Controllers
{
    public class MarketingManagementController : Controller
    {
        // GET: MarkettingManager
        public ActionResult Index()
        {
            return View();
        }

        // GET: MarkettingManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MarkettingManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarkettingManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarkettingManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MarkettingManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarkettingManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MarkettingManager/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
