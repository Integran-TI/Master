using Applications.IntegranTI.com.Models;
using System.Web.Mvc;

namespace Applications.IntegranTI.com.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly Companies Item;

        public CompaniesController()
        {
            Item = new Companies();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
            
        [HttpGet]
        public ActionResult List()
        {
            return View("List", Item.SelectItems());
        }

        [HttpPost]
        public ActionResult View(FormCollection formCollection)
        {
            if (formCollection["ButtonEdit"] != null)
            {
                return View("Edit", Item.SelectItem(int.Parse(formCollection["ID_Company"])));
            }
            else if (formCollection["ButtonList"] != null)
            {
                //return View("List", Item.SelectItems());
                return RedirectToAction("List");
            }
            else
            {
                return View("View", Item.SelectItem(int.Parse(formCollection["ID_Company"])));
            }
        }

        [HttpPost]
        public ActionResult Add(FormCollection formCollection)
        {
            if (formCollection["ButtonSave"] != null)
            {
                Item.ID_Company = int.Parse(formCollection["ID_Company"]);
                Item.Name = formCollection["Name"];
                Item.FullName = formCollection["FullName"];
                Item.InsertItem(Item);

                return View("View", Item);
            }
            else if (formCollection["ButtonList"] != null)
            {
                //return View("List", Item.SelectItems());
                return RedirectToAction("List");

            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            if (formCollection["ButtonSave"] != null)
            {
                Item.ID_Company = int.Parse(formCollection["ID_Company"]);
                Item.Name = formCollection["Name"];
                Item.FullName = formCollection["FullName"];
                Item.UpdateItem(Item);

                return View("View", Item);
            }
            else if (formCollection["ButtonList"] != null)
            {
                return View("View", Item.SelectItem(int.Parse(formCollection["ID_Company"])));
            }
            else
            {
                return View("Edit", Item.SelectItem(int.Parse(formCollection["ID_Company"])));
            }
        }

        [HttpPost]
        public ActionResult Delete(FormCollection formCollection)
        {
            if (formCollection["ButtonSave"] != null)
            {
                Item.ID_Company = int.Parse(formCollection["ID_Company"]);
                Item.DeleteItem(Item);

                return View("List", Item.SelectItems());
            }
            else if (formCollection["ButtonView"] != null)
            {
                return View("View", Item.SelectItem(int.Parse(formCollection["ID_Company"])));
            }
            else
            {
                return View("Delete", Item.SelectItem(int.Parse(formCollection["ID_Company"])));
            }
        }

    }
}