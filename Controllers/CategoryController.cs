using dotnetsorting.Tools;
using InventoryManger.Data;
using InventoryManger.Interfaces;

using InventoryManger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManger.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly ICategory _Repo;

        public CategoryController(ICategory Repo)
        {
            _Repo = Repo;
        }

        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;

            PaginatedList<Category> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            //int totRecs=((PaginatedList<Unit>)units).TotalRecord;

            var pager = new PagerModel(items.TotalRecord, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);
        }

        public IActionResult Create()
        {
            Category items = new Category();
            return View(items);
        }
        [HttpPost]
        public IActionResult Create(Category items)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (items.Description.Length < 4 || items.Description == null)
                    errMessage = "Description Must be atleast 4 Characters";

                if (_Repo.IsItemExist(items.Name) == true)
                    errMessage = errMessage + " "  + items.Name + " " + "Exists Already";

                if (errMessage == "")
                {
                    items = _Repo.Create(items);
                    bolret = true;
                }
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }
            if (bolret == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(items);
            }
            else
            {

            }
            TempData["SuccessMessage"] = items.Name + " "+ "Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        //edit 
        public IActionResult Edit(int id)
        {
           Category items = _Repo.GetItem(id);
            TempData.Keep();
            return View(items);
        }
        [HttpPost]
        public IActionResult Edit(Category items)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (items.Description.Length < 4 || items.Description == null)

                    errMessage = "Description Must be atleast 4 Character";

                if (_Repo.IsItemExist(items.Name, items.Id) == true)
                    errMessage = errMessage + " " + items.Name + "Exists Already";

                if (errMessage == "")
                {
                    items = _Repo.Edit(items);
                    TempData["SuccessMessage"] =  items.Name + "Updated Successfully";
                    bolret = true;
                }

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
           Category items = _Repo.GetItem(id);
            return View(items);
        }
        //delete
        public IActionResult Delete(int id)
        {

            Category items = _Repo.GetItem(id);
            TempData.Keep();
            return View(items);
        }
        [HttpPost]
        public IActionResult Delete(Category items)
        {
            try
            {
                items = _Repo.Delete(items);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(items);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            TempData["SuccessMessage"] =  items.Name + " " +"Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });


        }
    }
}
