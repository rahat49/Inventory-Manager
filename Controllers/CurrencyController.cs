using dotnetsorting.Tools;
using Inventory_Manager.Models;
using InventoryManger.Interfaces;
using InventoryManger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InventoryManger.Controllers
{
    [Authorize]
    public class CurrencyController : Controller
    {

        private readonly ICurrency _Repo;

        public CurrencyController(ICurrency Repo)
        {
            _Repo = Repo;
        }

        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.AddColumn("description");
            sortModel.AddColumn("exchangeCurrency");
            sortModel.AddColumn("exchngRate");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;

            PaginatedList<Currency> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);


            //int totRecs=((PaginatedList<Unit>)units).TotalRecord;

            var pager = new PagerModel(items.TotalRecord, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);
        }

        public IActionResult Create()
        {
            Currency items = new Currency();
            return View(items);
        }
        [HttpPost]
        public IActionResult Create(Currency items)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                bolret= _Repo.Create(items);
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }
            if (bolret == false)
            {
                errMessage =errMessage + " " + _Repo.GetError();
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(items);
            }
            else
            {
                TempData["SuccessMessage"] = items.Name + " " + "Created Successfully";
                return RedirectToAction(nameof(Index));
            }
           
        }
        //edit 
        public IActionResult Edit(int id)
        {
            Currency items = _Repo.GetItem(id);
            TempData.Keep();
            return View(items);
        }
        [HttpPost]
        public IActionResult Edit(Currency items)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                bolret = _Repo.Edit(items);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
            }

            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];

            if (bolret == false)
            {
                errMessage = errMessage + " " + _Repo.GetError();
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(items);
            }
            else

                TempData["SuccessMessage"] = items.Name + " " + "Updated Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
        public IActionResult Details(int id)
        {
            Currency items = _Repo.GetItem(id);
            return View(items);
        }
        //delete
        public IActionResult Delete(int id)
        {

            Currency items = _Repo.GetItem(id);
            TempData.Keep();
            return View(items);
        }
        [HttpPost]
        public IActionResult Delete(Currency items)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
               bolret=_Repo.Delete(items);
            }
            catch (Exception ex)
            {
                 errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(items);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];

            if (bolret == false)
            {
                errMessage = errMessage + " " + _Repo.GetError();
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(items);
            }
            else
            {
                TempData["SuccessMessage"] = items.Name + " " + "Deleted Successfully";
                return RedirectToAction(nameof(Index), new { pg = currentPage });
            }
           


        }
    }
}
