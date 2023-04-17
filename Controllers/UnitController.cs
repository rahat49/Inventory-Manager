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
    public class UnitController : Controller
    {

        private readonly IUnit _unitRepo;

        public UnitController(IUnit unitRepo)
        {
            _unitRepo = unitRepo;
        }
        
        public IActionResult Index(string sortExpression="", string SearchText = "",int pg=1, int pageSize=5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("name");
            sortModel.AddColumn("description");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"]= sortModel;
            ViewBag.SearchText = SearchText;

            PaginatedList<Unit> units = _unitRepo.GetItems(sortModel.SortedProperty,sortModel.SortedOrder, SearchText, pg, pageSize);

            //int totRecs=((PaginatedList<Unit>)units).TotalRecord;

            var pager = new PagerModel(units.TotalRecord, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(units);
        }
       
        public IActionResult Create()
        {
            Unit items = new Unit();
            return View(items);
        }
        [HttpPost]
        public IActionResult Create(Unit unit)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (unit.Description.Length < 4 || unit.Description == null)
                    errMessage = "Unit Description Must be atleast 4 Characters";

                if (_unitRepo.IsUnitNameExist(unit.Name) == true)
                    errMessage = errMessage + " " + "Unit Name" + unit.Name + "Exists Already";
              
            if(errMessage=="")
                {
                    unit = _unitRepo.Create(unit);
                    bolret = true;
                }
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }
            if(bolret==false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(unit);
            }
            else
            {

            }
            TempData["SuccessMessage"] = "Unit" + unit.Name + "Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        //edit 
        public IActionResult Edit(int id)
        {
            Unit unit = _unitRepo.GetUnit(id);
            TempData.Keep();
            return View(unit);
        }
        [HttpPost]
        public IActionResult Edit(Unit unit)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if(unit.Description.Length<4 || unit.Description==null)
                
                    errMessage = "Unit Description Must be atleast 4 Character";

                if (_unitRepo.IsUnitNameExist(unit.Name,unit.Id) == true)
                    errMessage = errMessage + " " + "Unit Name" + unit.Name + "Exists Already";

                if (errMessage == "")
                {
                    unit = _unitRepo.Edit(unit);
                    TempData["SuccessMessage"] = "Unit" + unit.Name + "Updated Successfully";
                    bolret = true;
                }

            }
            catch(Exception ex)
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
                return View(unit);
            }
          else

            TempData["SuccessMessage"] = "Unit" + unit.Name + "Created Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage }); 
        }
        public IActionResult Details(int id)
        {
            Unit unit = _unitRepo.GetUnit(id);
            return View(unit);
        }
        //delete
        public IActionResult Delete(int id)
        {

            Unit unit = _unitRepo.GetUnit(id);
            TempData.Keep();
            return View(unit);
        }
        [HttpPost]
        public IActionResult Delete(Unit unit)
        {
            try
            {
                unit = _unitRepo.Delete(unit);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(unit);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            TempData["SuccessMessage"] = "Unit" + unit.Name + "Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });

        
        }
    }
}
